using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData {
    //Contains info/functions for populating game data

    public static float stageHeight;
    public static float stageBottom;
    public static float stageWidth;

    public static Attack CreateExampleJabAttack(List<GameObject> hb)
    {
        //MoveFrame constructor argument order: Startup, Endlag, Hitbox Active, List of Hitboxes, Last Frame,
        //                                      Is Animated, List of Animations, Autocancelable, Cancelable

        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 1 frame
        fd.Add(new MoveFrame(true, false, false));

        List<bool> activeHitboxes = new List<bool>()
        {
            true
        };
        //Active - 2 frames
        fd.Add(new MoveFrame(false, false, true, activeHitboxes));
        fd.Add(new MoveFrame(false, false, true, activeHitboxes));

        //Endlag - 12 frames
        for (int i = 0; i < 12; i++)
        {
            fd.Add(new MoveFrame(false, true, false));
        }

        //FAF - 16
        fd.Add(new MoveFrame(false, false, false, null, true));

        Attack exampleJabAttack = new Attack(hb, fd);

        return new Attack(hb,fd);
    }

    public static Attack CreateExampleFTiltAttack(List<GameObject> hb)
    {
        //MoveFrame constructor argument order: Startup, Endlag, Hitbox Active, List of Hitboxes, Last Frame,
        //                                      Is Animated, List of Animations, Autocancelable, Cancelable

        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 5 frames
        for (int i = 0; i < 5; i++)
        {
            fd.Add(new MoveFrame(true, false, false));
        }

        List<bool> activeHitboxes = new List<bool>()
        {
            true,
            true
        };
        //Active - 2 frames
        fd.Add(new MoveFrame(false, false, true, activeHitboxes));
        fd.Add(new MoveFrame(false, false, true, activeHitboxes));

        //Endlag - 22 frames
        for (int i = 0; i < 22; i++)
        {
            fd.Add(new MoveFrame(false, true, false));
        }

        //FAF - 25
        fd.Add(new MoveFrame(false, false, false, null, true));

        return new Attack(hb, fd);
    }

    public static Attack CreateExampleUTiltAttack(List<GameObject> hb)
    {
        //MoveFrame constructor argument order: Startup, Endlag, Hitbox Active, List of Hitboxes, Last Frame,
        //                                      Is Animated, List of Animations, Autocancelable, Cancelable

        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 10 frames
        for (int i = 0; i < 10; i++)
        {
            fd.Add(new MoveFrame(true, false, false));
        }

        List<bool> activeHitboxes = new List<bool>()
        {
            true,
            true,
        };

        //List of hitbox position changes by frame
        //frame 2
        List<Vector3> frame2Anim = new List<Vector3>()
        {
            new Vector3(0.7F, 0.8F),
            new Vector3(1.4F, 1F)
        };
        //frame 3
        List<Vector3> frame3Anim = new List<Vector3>()
        {
            new Vector3(0.7F, -0.8F),
            new Vector3(1.4F, -1F)
        };

        //Active - 3 frames
        fd.Add(new MoveFrame(false, false, true, activeHitboxes));
        fd.Add(new MoveFrame(false, false, true, activeHitboxes, false, true, frame2Anim));
        fd.Add(new MoveFrame(false, false, true, activeHitboxes, false, true, frame3Anim));

        //Endlag - 20 frames
        for (int i = 0; i < 23; i++)
        {
            fd.Add(new MoveFrame(false, true, false));
        }

        //FAF - 43
        fd.Add(new MoveFrame(false, false, false, null, true));

        return new Attack(hb, fd);
    }

    public static Attack CreateExampleDTiltAttack(List<GameObject> hb)
    {
        //MoveFrame constructor argument order: Startup, Endlag, Hitbox Active, List of Hitboxes, Last Frame,
        //                                      Is Animated, List of Animations, Autocancelable, Cancelable

        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 6 frames
        for (int i = 0; i < 6; i++)
        {
            fd.Add(new MoveFrame(true, false, false));
        }

        List<bool> activeHitboxes = new List<bool>()
        {
            true,
            true,
            true
        };
        //Active - 2 frames
        fd.Add(new MoveFrame(false, false, true, activeHitboxes));
        fd.Add(new MoveFrame(false, false, true, activeHitboxes));

        //Endlag - 20 frames
        for (int i = 0; i < 20; i++)
        {
            fd.Add(new MoveFrame(false, true, false));
        }

        //FAF - 29
        fd.Add(new MoveFrame(false, false, false, null, true));

        return new Attack(hb, fd);
    }

    public static Attack CreateExampleNAirAttack(List<GameObject> hb)
    {
        //MoveFrame constructor argument order: Startup, Endlag, Hitbox Active, List of Hitboxes, Last Frame,
        //                                      Is Animated, List of Animations, Autocancelable, Cancelable

        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 3 frames
        for (int i = 0; i < 3; i++)
        {
            fd.Add(new MoveFrame(true, false, false));
        }

        List<bool> activeHitboxes = new List<bool>()
        {
            true,
            false
        };

        //Active - 23 frames (3 early, 20 late)
        for (int i = 0; i < 3; i++)
        {
            fd.Add(new MoveFrame(false, false, true, activeHitboxes));
        }

        activeHitboxes = new List<bool>()
        {
            false,
            true
        };

        for (int i = 0; i < 20; i++)
        {
            fd.Add(new MoveFrame(false, false, true, activeHitboxes));
        }


        //Endlag - 15 frames (autocancel 10 frames)
        for (int i = 0; i < 5; i++)
        {
            fd.Add(new MoveFrame(false, true, false));
        }

        for (int i = 0; i < 10; i++)
        {
            fd.Add(new MoveFrame(false, true, false, null, false, false, null, true));
        }

        //FAF - 42
        fd.Add(new MoveFrame(false, false, false, null, true));

        Attack nAirAttack = new Attack(hb, fd);
        nAirAttack.aerial = true;
        nAirAttack.landingLag = 10;


        return nAirAttack;
    }

    public static Attack CreateExampleDAirAttack(List<GameObject> hb)
    {
        //MoveFrame constructor argument order: Startup, Endlag, Hitbox Active, List of Hitboxes, Last Frame,
        //                                      Is Animated, List of Animations, Autocancelable, Cancelable

        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 14 frames
        for (int i = 0; i < 14; i++)
        {
            fd.Add(new MoveFrame(true, false, false));
        }

        List<bool> activeHitboxes = new List<bool>()
        {
            true,
            true,
            true
        };

        //Active - 4 frames
        for (int i = 0; i < 4; i++)
        {
            fd.Add(new MoveFrame(false, false, true, activeHitboxes));
        }

        //Endlag - 30 frames (autocancel 10 frames)
        for (int i = 0; i < 20; i++)
        {
            fd.Add(new MoveFrame(false, true, false));
        }

        for (int i = 0; i < 10; i++)
        {
            fd.Add(new MoveFrame(false, true, false, null, false, false, null, true));
        }

        //FAF - 48
        fd.Add(new MoveFrame(false, false, false, null, true));

        Attack dAirAttack = new Attack(hb, fd);
        dAirAttack.aerial = true;
        dAirAttack.landingLag = 18;


        return dAirAttack;
    }

    public static Attack CreateExampleUAirAttack(List<GameObject> hb)
    {
        //MoveFrame constructor argument order: Startup, Endlag, Hitbox Active, List of Hitboxes, Last Frame,
        //                                      Is Animated, List of Animations, Autocancelable, Cancelable

        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 9 frames
        for (int i = 0; i < 9; i++)
        {
            fd.Add(new MoveFrame(true, false, false));
        }

        List<bool> activeHitboxes = new List<bool>()
        {
            true,
            true,
            true
        };

        //Active - 6 frames
        for (int i = 0; i < 6; i++)
        {
            fd.Add(new MoveFrame(false, false, true, activeHitboxes));
        }

        //Endlag - 30 frames (autocancel 10 frames)
        for (int i = 0; i < 20; i++)
        {
            fd.Add(new MoveFrame(false, true, false));
        }

        for (int i = 0; i < 10; i++)
        {
            fd.Add(new MoveFrame(false, true, false, null, false, false, null, true));
        }

        //FAF - 46
        fd.Add(new MoveFrame(false, false, false, null, true));

        Attack uAirAttack = new Attack(hb, fd);
        uAirAttack.aerial = true;
        uAirAttack.landingLag = 15;


        return uAirAttack;
    }

    public static Attack CreateExampleFAirAttack(List<GameObject> hb)
    {
        //MoveFrame constructor argument order: Startup, Endlag, Hitbox Active, List of Hitboxes, Last Frame,
        //                                      Is Animated, List of Animations, Autocancelable, Cancelable

        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 5 frames
        for (int i = 0; i < 5; i++)
        {
            fd.Add(new MoveFrame(true, false, false));
        }

        List<bool> activeHitboxes = new List<bool>()
        {
            true,
            true
        };

        //Active - 3 frames
        for (int i = 0; i < 3; i++)
        {
            fd.Add(new MoveFrame(false, false, true, activeHitboxes));
        }

        //Endlag - 25 frames (autocancel 1 frames)
        for (int i = 0; i < 20; i++)
        {
            fd.Add(new MoveFrame(false, true, false));
        }

        for (int i = 0; i < 1; i++)
        {
            fd.Add(new MoveFrame(false, true, false, null, false, false, null, true));
        }

        //FAF - 34
        fd.Add(new MoveFrame(false, false, false, null, true));

        Attack fAirAttack = new Attack(hb, fd);
        fAirAttack.aerial = true;
        fAirAttack.landingLag = 13;


        return fAirAttack;
    }

    public static Attack CreateExampleBAirAttack(List<GameObject> hb)
    {
        //MoveFrame constructor argument order: Startup, Endlag, Hitbox Active, List of Hitboxes, Last Frame,
        //                                      Is Animated, List of Animations, Autocancelable, Cancelable

        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 7 frames
        for (int i = 0; i < 5; i++)
        {
            fd.Add(new MoveFrame(true, false, false));
        }

        List<bool> activeHitboxes = new List<bool>()
        {
            true,
            true
        };

        //frame 2
        List<Vector3> frame2Anim = new List<Vector3>()
        {
            new Vector3(0.2F, 0.4F),
            new Vector3(0.4F, 0.8F)
        };
        //frame 3
        List<Vector3> frame3Anim = new List<Vector3>()
        {
            new Vector3(0.1F, 0.5F),
            new Vector3(0.4F, 0.8F)
        };
        //frame 4
        List<Vector3> frame4Anim = new List<Vector3>()
        {
            new Vector3(0F, 0.4F),
            new Vector3(0.1F, 0.6F)
        };

        //Active - 4 frames
        fd.Add(new MoveFrame(false, false, true, activeHitboxes));
        fd.Add(new MoveFrame(false, false, true, activeHitboxes, false, true, frame2Anim));
        fd.Add(new MoveFrame(false, false, true, activeHitboxes, false, true, frame3Anim));
        fd.Add(new MoveFrame(false, false, true, activeHitboxes, false, true, frame4Anim));


        //Endlag - 16 frames (autocancel 8 frames)
        for (int i = 0; i < 8; i++)
        {
            fd.Add(new MoveFrame(false, true, false));
        }

        for (int i = 0; i < 8; i++)
        {
            fd.Add(new MoveFrame(false, true, false, null, false, false, null, true));
        }

        //FAF - 32
        fd.Add(new MoveFrame(false, false, false, null, true));

        Attack bAirAttack = new Attack(hb, fd);
        bAirAttack.aerial = true;
        bAirAttack.landingLag = 14;


        return bAirAttack;
    }
}


/* 
 *      How to create a new Attack:
 *      
 * First, make a new Function titled "Create ________ Attack(List<GameObject hb)" above.
 * Then, go into the Scene and create Hitboxes to populate hb with. These will go in a list that you create in GameLoader.
 * You can add a container in PlayerController and a reference to this function to the GameLoader main script at this point as well. 
 * Finally, populate the framedata and properties of the move. Done!      
*/