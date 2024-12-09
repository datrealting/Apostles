using UnityEngine;

// this defines how the status effect should react to being applied to a target already with the effect.

// STACK: apply another one, separate of the first
// EXTEND: add the duration of the new one to the old one
// REFRESH: renew the duration to it's maximum

public enum OverrideType
{
    Stack,
    Extend,
    Refresh,
    Nothing
}
