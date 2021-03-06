# 2022_Skill_Contest

## **2022 서울지방기능경기대회 게임개발 종목 은상 수상작**

# [동영상 링크](https://www.youtube.com/watch?v=NVz6qPCEk5w)

##### 개발시간 18시간

### 과제 기획 (+표시는 기획서의 내용이 아닌 추가 기획)

#### 배경

##### 스토리 : 코로나 시대, 백신 로봇은 세균과 박테리아를 살균하고 코로나와 변종 코로나를 처치해야한다.
##### ENTITY : 백신 로봇, 세균, 박테리아, 백혈구, 적혈구, 코로나, 변종 코로나
##### => 백신 로봇과 세균은 슈팅 게임에 친숙할 수 있도록 전투기 모양처럼 제작

------------

#### 게임 구성

##### 장르 : 슈팅, 탄막
##### 씬 구성 : 타이틀 => 스테이지 1 => 스테이지 2 => 랭킹
##### 고통게이지 : 적들이 지나갈 경우 || 적혈구를 보호하지 못할 경우 => Percent++
##### 체력게이지 : 플레이어의 체력
##### 게임오버 : 고통게이지가 100% || 체력게이지가 0% 일 경우
##### 데이터 : 적들과 플레이어의 스텟은 클래스 형식으로 외부에서 수정할 수 있어야 한다. / 랭킹(점수)의 유지는 게임 클라이언트가 종료되기 전까지 유지되어야 한다.

##### +보스 등장 조건 : 적 통합 200마리가 처치 or 지나갈 시 보스 등장

------------

#### 플레이어

##### 체력게이지 : 플레이어의 체력
##### +연료게이지 : 플레이어가 부스터와 저공 비행을 사용하기 위한 게이지 (0%가 될 경우 체력 감소 및 3초간 과부하 상태 진입)
##### 부스터 : 플레이어의 이동속도를 2배로 함
##### 저공 비행 : 스페이스바를 누른동안 저공 비행하며 무적 상태로 전환

#### 레벨
##### 기본 공격 강화 : 3/5/7/9 레벨이 될 때 마다 플레이어의 기본 공격 강화
##### +스킬 : 레벨업 할 때 마다 플레이어는 랜덤 스킬 3종 중 하나 선택 가능
##### +(종류 : 원형탄, 총알반사, 추적공격, 추적발사, 레이저, 흝뿌리기)

------------

#### ENTITY
##### 세균 : 적은 일직선 상으로 직진하며 총알을 발사한다.
##### +박테리아 : 적은 등장 패턴이 존재하며 2가지의 패턴 실행 후 오른쪽 또는 왼쪽으로 퇴장한다.
##### 백혈구 : 플레이어가 처치시 4가지중 랜덤으로 이로운 아이템을 실행 (체력게이지 증가 / 고통게이지 감소 / 레벨업 / 무적 3초)
##### 적혈구 : 적혈구가 처치 될시 고통게이지 특정 값 증가
##### 코로나 바이러스 : 보스는 유저가 재미를 느낄 수 있도록 디자인 한다.
##### +코로나 : 3가지의 패턴 | 원형으로 발사 / 6방향으로 다수 총알 발사 / 원형으로 발사 후 정지 => 0.5초뒤 플레이어 방향으로 발사
##### +변종코로나 : 5가지의 패턴 | 기존 패턴 / 삼각함수를 이용한 패턴 2가지

------------
------------

### 스크립트 패턴 사용

##### 적, 엔티티 : EnemySubject와 EntitySubject를 통해 적들을 옵저버 패턴으로 관리하여 적들에게 변경사항을 전달합니다. [[EnemySubject](https://github.com/hariharu1221/2022_Skill_Contest/blob/main/Assets/Scripts/Enemy/EnemySubject.cs)] [[EntitySubject](https://github.com/hariharu1221/2022_Skill_Contest/blob/main/Assets/Scripts/Entity/EntitySubject.cs)]

##### 총알 : BulletSubject를 통해 옵저버 패턴으로 적을 관리하고 / BulletPooling으로 총알을 생성 및 폴링 해 GC의 호출을 줄임 => 스파이크 현상 방지
##### [[BulletSubject](https://github.com/hariharu1221/2022_Skill_Contest/blob/main/Assets/Scripts/Bullet/BulletSubject.cs)] [[BulletPooling](https://github.com/hariharu1221/2022_Skill_Contest/blob/main/Assets/Scripts/Bullet/BulletPooling.cs)]

##### 데이터 : JsonLoader 클래스의 함수를 static과 Generic으로 구현해 접근성과 사용성이 편리하도록 제작하였습니다. [[JsonLoader](https://github.com/hariharu1221/2022_Skill_Contest/blob/main/Assets/Scripts/Utils/JsonLoader.cs)]

##### 싱글톤 : 옵저버나 게임매니저 등 하나만 존재해야하거나 데이터 공유나 접근이 쉬워야할 때 Generic으로 적용함.

##### 적 : 상속패턴을 통해 필요한 기능을 재정의해 다른 적이더라도 Enemy 클래스를 상속 받았다면 옵저버에서 탐색 가능

------------
------------

![ezgif-1-cd412f568a](https://user-images.githubusercontent.com/67540874/165127085-c83fa8dd-9510-472d-84a6-aa2472a79991.gif)
![ezgif-1-66d5a29b51](https://user-images.githubusercontent.com/67540874/165127350-d834fd0c-52ad-4479-8a3a-5600342eae54.gif)

[동영상 링크](https://www.youtube.com/watch?v=NVz6qPCEk5w)
