namespace Hospital.Application.Enums;

public enum DateParamsPrefix
{
    // matches (obviously)
    eq,
    //not in the range
    ne,
    // less than date
    lt,
    // great than date
    gt,
    // great or equal date
    ge,
    // less or equal date
    le,
    // start at date
    sa,
    // end before date
    eb,
    // as it exactly matches
    ap,
    // unknown
    unknown
}