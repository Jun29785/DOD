using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOD.Define
{

    public enum Scenes
    {
        LobbyScene,
        GameScene,
        InventoryScene,
        LoadingScene
    }

    public enum skillEnum
    {
        연속찌르기 = 10001,
        돌진,
        회전공격,
    }
    
    public enum playerCharacter 
    {
        Knight,

    }

    public enum IntroPhase
    {
        Start,
        ApplicationSetting,
        StaticData, // 기획데이터 초기화 및 로드
        UserData, // 유저데이터 로드
        Compelte
    }
    class Define
    {
    }
}
