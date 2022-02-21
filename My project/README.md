# 유니티 엔진

## 단축키

```
ctrl + shift + n = GameObject
ctrl + shift + c = Console
ctrl + shift + f = 메인 카메라 이동
ctrl + p = play
```

## 싱글톤

```
MonoBehaviour 클래스를 상속해야만 컴포넌트로 붙일 수 있어진다.
static instance를 선언하고, Start할 때 instance를 넣어준다.
Start할 때 넣어주면 파일이 없을 때 에러가 날 수 있기 때문에 Init()을 만들어 넣어준다
GameObject.Find("@Managers");로 찾아보고 없다면 new GameObject{name="@Managers"};로 만들어준다
AddComponent<Managers>();로 스크립트를 go에 넣어주고 go에서 GetComponenet<Managers>();로 인스턴스에 가져온다.
프로퍼티로 변경하여 함수형 호출이 아니도록 만들어준다.
```

## Transform

```
Input.GetKey(KeyCode.W))로 W 누름을 확인할 수 있다.
Time.delta를 방향에 곱해 프레임마다 입력할 수 있도록 만든다.
speed변수까지 곱해줘서 스피드를 조절할 수 있도록 만들어준다.
[SerializedFiled] 태그를 붙여주면 public으로 선언했을 때 유니티 엔진에 나타나는 것처럼 나타나지만 private인 상태인 변수로 만들어준다
world 좌표로 벡터가 적용되기 때문에 Rotation시 어색해진다.
TransfomrDirection -> World -> Local 좌표계로 변경해준다
transform.Translate()안에 넣어줘서 변경하는것 권장
오일러 앵글은 절대값으로 회전할때, Rotate함수는 변화하는 회전일때? 사용
transform.rotation => 회전값 조정, Quaternion 형식으로 조정
transform.position => 위치값 조정, Vector 형식으로 조정
업데이트에서 인풋 체크를 너무 많이하는것도 부하 -> Action deligate로 해결(KeyAction으로 구독)
```

## Prefab

```
한번 만든걸 저장하면 모두 담겨져서 나옴
Nested Prefab은 Prefab을 중첩시키는 것
Resources폴더 안에 넣어두면 Resources 클래스에서 Load할 수 있다.
```

## Collision

```
Rigidbody 컴포넌트 사용
collider는 양쪽에 모두 있어야 멈춤
OnCollisionEnter 조건
 - 나한테 RigidBody가 있어야 함(IsKinematic: Off)
 - 둘 다 Collider가 있어야 함(IsTrigger: Off)
OnTriggerEnter 조건
 - 둘 다 Collider가 있어야 함
 - 둘 중 하나는 IsTrigger: On
 - 둘 중 하나는 RigidBody가 있어야 함
Physics.Raycast로 감지 가능
Screen좌표계 - 화면상의 픽셀단위
Viewport좌표계 - Screen좌표계를 비율로 변환한것
Input.GetMousbutton(0) - 왼클릭 체크
Raycast를 이용 - Vector3 or Ray 구조체를 이용함
LayerMask 구조체를 통해 Ray를 지정해줄 수도 있다
```

## 카메라

```
RayCast로 플레이어부터 거리를 재고, 그 사이에 hit 되는게 있다면 카메라를 조정도 해줌
position을 플레이어 움직임을 따라 고정시킨다
Update()로 만들면 작동 순서가 뒤죽박죽이 될 수 있어서 LateUpdate()로 함수 변경
LookAt()은 목적지를 향해 바로 쳐다본다.
```

## Animation

```
AnimatorController 파일이 따로 존재함 (C# 스크립트 파일이 아님)
AnimatorController에서 모두 처리하는 것이 아니라 Animation만 넣고 그 동작들을 스크립트에서 제어하는게 의외였음
 -> 알고보니 AnimatorController에서 처리하는게 편함(?)
 -> 파라미터를 transition의 condition에 넣고, 파라미터만 스크립트에서 관리하는게 굿
Animator 구조체로 컴포넌트를 가져오고 Play로 동작을 수행시킴
Blend 하여 애니메이션을 부드럽게 만들어줌
float 변수를 하나 선언해서 Lerp를 통해 천천히 변화시키고, 그 변수를 SetFloat해줌
애니메이션이 많아질수록 구분해야하는 상태 변수가 많아지기때문에 Enum으로 구분해서 함수로 나눠줌
```

## UI

```
버튼 안에 OnClick 콜백기능을 유니티 자체에서 연결 가능 -> 규모가 커지면 연결하는것만으로 관리하기 어렵기 때문에 코드로 자동화 구현
EventSystem.current.isPointerOverGameObject()로 UI가 클릭되는지 알 수 있음
 -> UI 클릭시 다른 동작(움직임)을 하지 않도록 해야함
Manager.Resource.Instantiate로 prefab을 불러오게끔 해놨음
enum을 사용함(Texts, Buttons, Images, GameObjects)
Bind로 enum과 실제 오브젝트랑 묶어준다 이 때 Reflection 기법으로 Bind, <T>로 컴포넌트 까지 구분
Dictionary(Type, Object[])로 오브젝트 목록 유지, FindChild를 구현 목록 찾음
Get함수로 오브젝트를 가져온다 이 때 만들어둔 이벤트 핸들러 Action에 등록한다
UI Manager를 통해 Sort Order를 조정한다
```

## Scene

```
Start는 컴포넌트가 켜져있어야만 하지만, Awake는 꺼져있어도 가능(Start보다 빠르다)
SceneManager가 이미 있어 Ex를 붙여 새로 정의
SceneManager.LoadScene(string)으로 가능
```

## Sound

```
AudioSound, AudioClip, AudioListener가 있어야 함
리스너는 한 씬에 하나만 있으면 되고, 대부분 카메라에 달려있음
PlayOneShot 함수는 한꺼번에 같이 나오게 할 수도 있음
rect transform이 아닌 오브젝트들은 SetParent가 아니라 parent 프로퍼티로 부모를 지정해준다.
Dictionary의 TryGetValue 함수로 캐싱도 설계함
하나의 함수에서 다른 버전의 함수를 응용하는 방법을 활용하자
```

## Pooling

```
매니저.리소스.인스티시에이트 하던걸 더 성능이 좋게끔 만듬
컴포넌트로 풀하는 오브젝트인지 아닌지 구분함
프리팹에서 드래그 드롭하는걸 코드로 한다고 생각하면 됨
```

## Coroutine(코루틴)

```
엄청 오래 걸리는 작업을 잠시 끊거나, 원하는 타이밍에 함수를 잠시 멈춤/복원 하는 경우
return -> 원하는 타입으로 가능
yield return은 일시적인 정지이고, yield break가 절대적인 종료
시간 관리에 있어서 엄청난 강점이 있음, WaitForSeconds와 StartCoroutine을 이용함

```
