# 4. Factory Pattern (팩토리 패턴)
## 개념
객체 생성을 new 키워드로 직접 하지 않고, 생성 책임을 Factory 클래스에 위임하는 패턴

**다형성(polymorphism)**과 함께 사용되면 매우 유연해짐

## 왜 쓰는가?
객체 생성 로직을 숨기고 관리하기 쉬움

객체 종류가 다양하거나 생성 방식이 복잡할 때 유리

변경에 강한 코드 구조

### 예제 시나리오: 메시지 박스 스타일을 바꾸는 팝업 생성기
#### 1. 팝업 인터페이스
```cs
public interface ICustomPopup
{
    void Show(string message);
}
```
#### 2. 실제 팝업 구현체
```cs
public class InfoPopup : ICustomPopup
{
    public void Show(string message) =>
        MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
}

public class WarningPopup : ICustomPopup
{
    public void Show(string message) =>
        MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
}
```
#### 3. 팩토리 클래스
```cs
public enum PopupType
{
    Info,
    Warning
}

public class PopupFactory
{
    public static ICustomPopup Create(PopupType type)
    {
        return type switch
        {
            PopupType.Info => new InfoPopup(),
            PopupType.Warning => new WarningPopup(),
            _ => throw new NotImplementedException()
        };
    }
}
```
#### 4. WinForm에서 사용하는 예
```cs
private void btnShowPopup_Click(object sender, EventArgs e)
{
    ICustomPopup popup = PopupFactory.Create(PopupType.Warning);
    popup.Show("저장되지 않았습니다!");
}
```
## 요약
팩토리를 쓰면 new InfoPopup()처럼 구체 타입에 의존하지 않게 되어, 유지보수성과 테스트성이 향상.

예를 들어 나중에 ErrorPopup, CustomDialogPopup이 생겨도 팩토리만 고치면 됨.

## WinForms에서 자주 쓰는 Factory 예시
컨트롤 생성기 (Button, TextBox 등 여러 종류 생성 시)

폼 전환기 (FormFactory.Create("Login"))

로깅 핸들러, DB 커넥터 등도 적용 가능