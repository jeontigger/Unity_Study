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
```
