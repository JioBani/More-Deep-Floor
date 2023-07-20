using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.Data.Defenders
{
    public enum DefenderId
    {
        None = 0,
        
        //#. 지휘관
        루리엘 = 10,
        
        
        //#. elf
        엘프_None = 100,
        엘프_소서러 = 101,
        엘프_수호자 = 102,
        엘프_사냥꾼 = 103,
        엘프_사제 = 104,
        엘프_그림자 = 105,
        숲지기 = 107,
        비취전차 = 108,
        엘프_워리어 = 109,
        루리엘_그렌타 = 110,
        
        
        //#. 1코스트
        아트록스 = 1001,
        리븐 = 1002,
        하이머딩거 = 1003,
        라이즈 = 1004,
        케이틀린 = 1005,
        세나 = 1006,
        키아나 = 1007,
        드레이븐 = 1008,
        
        //#. 2코스트
        룰루 = 1010,
        신지드 = 1011,
        이즈리얼 = 1012,
        트리스타나 = 1013,
        트린다미어 = 1014,
        
        //#. 3코스트
        럼블 = 1110,
        베인  = 1111,
        세트 = 1112,
        올라프 = 1113,
        잔나 = 1114,
        
        //#. 4코스트
        샤코 = 1211,
        직스 = 1212,
        징크스 = 1213,
        크산테 = 1214,
        
        //#. 5코스트
        다리우스 = 1310,
        레나타 = 1311,
        조이 = 1312,
        진 = 1313,
        
        
        Knight_Cost1,
        Rook_Cost1,
        Bishop_Cost1,
        
        Knight_Cost2,
        Rook_Cost2,
        Bishop_Cost2,
        
        Knight_Cost3,
        Rook_Cost3,
        Bishop_Cost3,
        
        Knight_Cost4,
        Rook_Cost4,
        Bishop_Cost4,
        
        Knight_Cost5,
        Rook_Cost5,
        Bishop_Cost15
    }
}
