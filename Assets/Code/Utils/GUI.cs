using System.Collections.Generic;

public static class GUIConstants {
    public static readonly Dictionary<Effect, string> EffectSpriteMapping = new() {
        [Effect.Damage] = "<sprite index=9>",
        [Effect.Poison] = "<sprite index=8>",
        [Effect.Heal] = "<sprite index=0>",
        [Effect.ActionPoint] = "<sprite index=3>",
        [Effect.Shield] = "<sprite index=6>",
    };
}
