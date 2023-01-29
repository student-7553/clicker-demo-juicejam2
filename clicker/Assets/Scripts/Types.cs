public enum BoardCellType
{
    GOLD_PER_CLICK,
    GOLD_PER_SEC,
    BOOST_ALL,
    BOOST_SPECIFIC
}

public enum BoardCellLevel
{
    LEVEL_1,
    LEVEL_2,
    LEVEL_3,
    LEVEL_4
}

public class BoardCellBaseStats
{
    int goldPerClickIncease;
    int goldPeSecIncease;
    int boostAdjacentAll;
}
