#  Zombie Shooting


Unity로 제작한 좀비 슈팅 게임 프로토타입 입니다.

여러가지 게임 시스템을 구현하기 위한 예제입니다.

---

## 프로젝트 구조
```
Assets/
└─ Scripts/
   ├─ Enemy/
   │   ├─ Enemy.cs                   # 상태 패턴 기반 적 AI
   │   ├─ IEnemyState.cs             # 상태 인터페이스
   │
   ├─ Inventory/
   │   ├─ InventorySystem.cs         # 인벤토리 관리
   │   ├─ Slot.cs                    # 인벤토리 내 아이템 슬롯
   │   ├─ EquipmentSlot.cs           # 장비창 관
   │
   ├─ Item/                          # 게임 내 아이템 관리
   │
   └─ Player/                        # 플레이어 입력 관리
```

## 주요 기능
-  **적 AI (상태 패턴)**  
    - Idle / Move / Chase / Attack / Dead 상태 기반 FSM(Finite State Machine)
    - 상태 전환이 명확해 유지보수 및 확장이 쉬움  

-  **인벤토리 시스템**  
    - 아이템 획득, 장착, 사용 기능  

-  **무기 시스템**  
    - 다양한 무기(도끼, 권총, 소총) 구현  
    - 무기별 공격 판정 및 애니메이션

-  **플레이어 시스템**  
    - New Input System 을 통한 인풋 관리

---
