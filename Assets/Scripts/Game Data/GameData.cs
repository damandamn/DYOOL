using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData {
    //Settings
    public static int stockCount = 4;
    public static bool visualizeHitboxes = true;
    public static float shieldDamageMultiplier = 0.25F;

    //Contains info/functions for populating game data
    public static GameObject hitboxRenderPrefab;
    public static bool comboCounter = true;

    public static GameObject ledgePrefab;

    public static float stageHeight;
    public static float stageBottom;
    public static float stageWidth;

    public static float ClampFloat(float num, bool neg = false)
    {
        if (!neg)
        {
            if (num < 0)
            {
                num = 0;
            }
        } else
        {
            if (num > 0)
            {
                num = 0;
            }
        }

        return num;
    }

    public static int ClampInt(int num, bool neg = false)
    {
        if (!neg)
        {
            if (num < 0)
            {
                num = 0;
            }
        }
        else
        {
            if (num > 0)
            {
                num = 0;
            }
        }

        return num;
    }


    //Defensive kit Options
    public static Attack CreateAirdodge(List<GameObject> hb)
    {

        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 2 frame
        for (int i = 0; i < 2; i++)
        {
            fd.Add(new MoveFrame()
            {
                startupFrame = true,
                playSound = true,
                frameSound = AudioContainer.whiff1
            });
        }

        //Invincible - 24 frames
        for (int i = 0; i < 24; i++)
        {
            fd.Add(new MoveFrame()
            {
                iFrame = true,
                frameAlpha = 0.5F
            });
        }

        //Endlag - 8 frames
        for (int i = 0; i < 8; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true
            });
        }

        //FAF - 25
        fd.Add(new MoveFrame()
        {
            lastFrame = true
        });

        Attack airdodge = new Attack(hb, fd);
        airdodge.aerial = true;
        airdodge.landingLag = 20;


        return airdodge;
    }

    public static Attack CreateDash(List<GameObject> hb)
    {

        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 2 frame

        fd.Add(new MoveFrame()
        {
            startupFrame = true,
            playSound = true,
            frameSound = AudioContainer.whiff1,
            userMovement = new Vector3(0, 0),
            setMomentum = true,
            userMomentum = new Vector3(0.135F, 0)
            });
        fd.Add(new MoveFrame()
        {
            startupFrame = true,
            playSound = true,
            frameSound = AudioContainer.whiff1,
            userMomentum = new Vector3(0.135F, 0)
        });

        //Invincible - 5 frames
        for (int i = 0; i < 5; i++)
        {
            fd.Add(new MoveFrame()
            {
                iFrame = true,
                frameAlpha = 0.5F
            });
        }

        //Endlag - 6 frames
        for (int i = 0; i < 6; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true
            });
        }

        //FAF - 13
        fd.Add(new MoveFrame()
        {
            lastFrame = true
        });

        Attack airdodge = new Attack(hb, fd);

        return airdodge;
    }

    public static Attack CreateBackDash(List<GameObject> hb)
    {

        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 2 frame
        fd.Add(new MoveFrame()
        {
            startupFrame = true,
            playSound = true,
            frameSound = AudioContainer.whiff1,
            userMovement = new Vector3(0, 0),
            setMomentum = true,
            userMomentum = new Vector3(-0.15F, 0)
        });
        fd.Add(new MoveFrame()
        {
            startupFrame = true,
            playSound = true,
            frameSound = AudioContainer.whiff1,
            userMomentum = new Vector3(-0.15F, 0)
        });

        //Invincible - 6 frames
        for (int i = 0; i < 6; i++)
        {
            fd.Add(new MoveFrame()
            {
                iFrame = true,
                frameAlpha = 0.5F
            });
        }

        //Endlag - 6 frames
        for (int i = 0; i < 6; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true
            });
        }

        //FAF - 14
        fd.Add(new MoveFrame()
        {
            lastFrame = true
        });

        Attack airdodge = new Attack(hb, fd);

        return airdodge;
    }


    //Example kit
    public static Attack CreateExampleJabAttack(List<GameObject> hb)
    {

        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 1 frame
        for (int i = 0; i < 1; i++)
        {
            fd.Add(new MoveFrame()
            {
                startupFrame = true,
                playSound = true,
                //frameSound = AudioContainer.hit
            });
        }

        List<bool> activeHitboxes = new List<bool>()
        {
            true
        };

        //Active - 2 frames
        for (int i = 0; i < 2; i++)
        {
            fd.Add(new MoveFrame()
            {
                hitboxActive = true,
                allHitboxesActive = activeHitboxes
            });
        }

        //Endlag - 18 frames
        for (int i = 0; i < 18; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true
            });
        }

        //FAF - 21
        fd.Add(new MoveFrame()
        {
            lastFrame = true
        });

        Attack exampleJabAttack = new Attack(hb, fd)
        {
            pierceShield = true
    };

        return exampleJabAttack;
    }

    public static Attack CreateExampleFTiltAttack(List<GameObject> hb)
    {
        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 10 frames
        for (int i = 0; i < 10; i++)
        {
            fd.Add(new MoveFrame()
            {
                startupFrame = true
            });
        }

        List<bool> activeHitboxes = new List<bool>()
        {
            true,
            true
        };
        //Active - 2 frames
        for (int i = 0; i < 2; i++)
        {
            fd.Add(new MoveFrame()
            {
                hitboxActive = true,
                allHitboxesActive = activeHitboxes
            });
        }

        //Endlag - 28 frames
        for (int i = 0; i < 28; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true
            });
        }

        //FAF - 40
        fd.Add(new MoveFrame()
        {
            lastFrame = true
        });

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
            fd.Add(new MoveFrame()
            {
                startupFrame = true
            });
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
        fd.Add(new MoveFrame()
        {
            hitboxActive = true,
            allHitboxesActive = activeHitboxes
        });

        fd.Add(new MoveFrame()
        {
            hitboxActive = true,
            allHitboxesActive = activeHitboxes,
            hitboxAnimated = true,
            allHitboxesAnimated = frame2Anim
        });

        fd.Add(new MoveFrame()
        {
            hitboxActive = true,
            allHitboxesActive = activeHitboxes,
            hitboxAnimated = true,
            allHitboxesAnimated = frame3Anim
        });

        //Endlag - 20 frames
        for (int i = 0; i < 23; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true
            });
        }

        //FAF - 43
        fd.Add(new MoveFrame()
        {
            lastFrame = true
        });

        return new Attack(hb, fd);
    }

    public static Attack CreateExampleDTiltAttack(List<GameObject> hb)
    {

        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 6 frames
        for (int i = 0; i < 6; i++)
        {
            fd.Add(new MoveFrame()
            {
                startupFrame = true
            });
        }

        List<bool> activeHitboxes = new List<bool>()
        {
            true,
            true,
            true
        };

        //Active - 2 frames
        for (int i = 0; i< 2; i++) {
            fd.Add(new MoveFrame()
            {
                hitboxActive = true,
                allHitboxesActive = activeHitboxes
            });
        }
        //Endlag - 20 frames
        for (int i = 0; i < 20; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true
            });
        }

        //FAF - 29
        fd.Add(new MoveFrame()
        {
            lastFrame = true
        });

        return new Attack(hb, fd);
    }

    public static Attack CreateExampleNAirAttack(List<GameObject> hb)
    {

        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 3 frames
        for (int i = 0; i < 3; i++)
        {
            fd.Add(new MoveFrame()
            {
                startupFrame = true
            });
        }

        List<bool> activeHitboxes = new List<bool>()
        {
            true,
            false
        };

        //Active - 20 frames (3 early, 17 late)
        for (int i = 0; i < 3; i++)
        {
            fd.Add(new MoveFrame()
            {
                hitboxActive = true,
                allHitboxesActive = activeHitboxes
            });
        }

        activeHitboxes = new List<bool>()
        {
            false,
            true
        };

        for (int i = 0; i < 18; i++)
        {
            fd.Add(new MoveFrame()
            {
                hitboxActive = true,
                allHitboxesActive = activeHitboxes
            });
        }


        //Endlag - 13 frames (autocancel 9 frames)
        for (int i = 0; i < 4; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true
            });
        }

        for (int i = 0; i < 9; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true,
                autoCancel = true
            });
        }

        //FAF - 42
        fd.Add(new MoveFrame()
        {
            lastFrame = true
        });

        Attack nAirAttack = new Attack(hb, fd);
        nAirAttack.aerial = true;
        nAirAttack.landingLag = 10;


        return nAirAttack;
    }

    public static Attack CreateExampleDAirAttack(List<GameObject> hb)
    {

        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 14 frames
        for (int i = 0; i < 14; i++)
        {
            fd.Add(new MoveFrame()
            {
                startupFrame = true
            });
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
            fd.Add(new MoveFrame()
            {
                hitboxActive = true,
                allHitboxesActive = activeHitboxes
            });
        }

        //Endlag - 30 frames (autocancel 10 frames)
        for (int i = 0; i < 20; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true
            });
        }

        for (int i = 0; i < 10; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true,
                autoCancel = true
            });
        }

        //FAF - 48
        fd.Add(new MoveFrame()
        {
            lastFrame = true
        });

        Attack dAirAttack = new Attack(hb, fd);
        dAirAttack.aerial = true;
        dAirAttack.landingLag = 20;


        return dAirAttack;
    }

    public static Attack CreateExampleUAirAttack(List<GameObject> hb)
    {

        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 8 frames
        for (int i = 0; i < 8; i++)
        {
            fd.Add(new MoveFrame()
            {
                startupFrame = true
            });
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
            fd.Add(new MoveFrame()
            {
                hitboxActive = true,
                allHitboxesActive = activeHitboxes
            });
        }

        //Endlag - 25 frames (autocancel 10 frames)
        for (int i = 0; i < 15; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true
            });
        }

        for (int i = 0; i < 10; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true,
                autoCancel = true
            });
        }

        //FAF - 39
        fd.Add(new MoveFrame()
        {
            lastFrame = true
        });

        Attack uAirAttack = new Attack(hb, fd);
        uAirAttack.aerial = true;
        uAirAttack.landingLag = 15;


        return uAirAttack;
    }

    public static Attack CreateExampleFAirAttack(List<GameObject> hb)
    {

        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 5 frames
        for (int i = 0; i < 5; i++)
        {
            fd.Add(new MoveFrame()
            {
                startupFrame = true
            });
        }

        List<bool> activeHitboxes = new List<bool>()
        {
            true,
            true
        };

        //Active - 3 frames
        for (int i = 0; i < 3; i++)
        {
            fd.Add(new MoveFrame()
            {
                hitboxActive = true,
                allHitboxesActive = activeHitboxes,
            });
        }

        //Endlag - 25 frames (autocancel 1 frames)
        for (int i = 0; i < 20; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true
            });
        }

        for (int i = 0; i < 1; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true,
                autoCancel = true
            });
        }

        //FAF - 34
        fd.Add(new MoveFrame()
        {
            lastFrame = true
        });

        Attack fAirAttack = new Attack(hb, fd);
        fAirAttack.aerial = true;
        fAirAttack.landingLag = 10;


        return fAirAttack;
    }

    public static Attack CreateExampleBAirAttack(List<GameObject> hb)
    {

        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 7 frames
        for (int i = 0; i < 6; i++)
        {
            fd.Add(new MoveFrame()
            {
                startupFrame = true
            });
        }

        fd.Add(new MoveFrame()
        {
            startupFrame = true,
            playSound = true,
            frameSound = AudioContainer.whiff2
        });

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
        fd.Add(new MoveFrame()
        {
            hitboxActive = true,
            allHitboxesActive = activeHitboxes
        });

        fd.Add(new MoveFrame()
        {
            hitboxActive = true,
            allHitboxesActive = activeHitboxes,
            hitboxAnimated = true,
            allHitboxesAnimated = frame2Anim
        });

        fd.Add(new MoveFrame()
        {
            hitboxActive = true,
            allHitboxesActive = activeHitboxes,
            hitboxAnimated = true,
            allHitboxesAnimated = frame3Anim
        });

        fd.Add(new MoveFrame()
        {
            hitboxActive = true,
            allHitboxesActive = activeHitboxes,
            hitboxAnimated = true,
            allHitboxesAnimated = frame4Anim
        });


        //Endlag - 16 frames (autocancel 8 frames)
        for (int i = 0; i < 8; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true
            });
        }

        for (int i = 0; i < 8; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true,
                autoCancel = true
            });
        }

        //FAF - 32
        fd.Add(new MoveFrame()
        {
            lastFrame = true
        });

        Attack bAirAttack = new Attack(hb, fd);
        bAirAttack.aerial = true;
        bAirAttack.landingLag = 14;


        return bAirAttack;
    }

    public static Attack CreateExampleNeutralBAttack(List<GameObject> hb)
    {

        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 40 frames (20 invincible, 20 not)
        fd.Add(new MoveFrame()
        {
            startupFrame = true,
            playSound = true,
            frameSound = AudioContainer.ringing1,
            canControl = true,
            canFall = true,
            iFrame = true
        });

        for (int i = 0; i < 19; i++)
        {
            fd.Add(new MoveFrame()
            {
                startupFrame = true,
                canControl = true,
                canFall = true,
                iFrame = true
            });
        }

        for (int i = 0; i < 20; i++)
        {
            fd.Add(new MoveFrame()
            {
                startupFrame = true,
                canControl = true,
                canFall = true,
            });
        }

        fd.Add(new MoveFrame()
        {
            startupFrame = true,
            playSound = true,
            frameSound = AudioContainer.whiff1,
            canControl = true,
            canFall = true,
        });

        List<bool> activeHitboxes = new List<bool>()
        {
            true,
            true
        };

        //Active - 4 frames
        for (int i = 0; i < 4; i++)
        {
            fd.Add(new MoveFrame()
            {
                hitboxActive = true,
                allHitboxesActive = activeHitboxes,
                canControl = true,
                canFall = true,
            });
        }


        //Endlag - 23 frames
        for (int i = 0; i < 23; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true,
                canControl = true,
                canFall = true,
            });
        }

        //FAF - 64
        fd.Add(new MoveFrame()
        {
            lastFrame = true,
            canControl = true,
            canFall = true,
        });

        Attack neutralBAttack = new Attack(true, hb, fd)
        {
            reverseFrames = 5
        };


        return neutralBAttack;
    }

    public static Attack CreateAerialExampleProjectileAttack(List<GameObject> hb)
    {
        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 29 frames
        for (int i = 0; i < 29; i++)
        {
            fd.Add(new MoveFrame()
            {
                startupFrame = true,
                canControl = true,
                canFall = true,
                cancelable = true
            });
        }

        fd.Add(new MoveFrame()
        {
            spawnProjectile = true,
            canControl = true,
            canFall = true,
            cancelable = true
        });

        //Endlag - 38 frames
        for (int i = 0; i < 38; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true,
                canControl = true,
                canFall = true,
                cancelable = true

            });
        }

        //FAF - 68
        fd.Add(new MoveFrame()
        {
            lastFrame = true,
            canControl = true,
            canFall = true
        });

        Attack exampleProjectileAttack = new Attack(true, hb, fd)
        {
            projectile = hb[0],
            destroyOnHit = true,
            reverseFrames = 6,
            groundCancel = true,
            landingLag = 20
        };

        return exampleProjectileAttack;
    }

    public static Attack CreateGroundedExampleProjectileAttack(List<GameObject> hb)
    {
        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 26 frames
        for (int i = 0; i < 26; i++)
        {
            fd.Add(new MoveFrame()
            {
                startupFrame = true,
                canControl = true,
                canFall = true,
            });
        }

        fd.Add(new MoveFrame()
        {
            spawnProjectile = true,
            canControl = true,
            canFall = true,
        });

        //Endlag - 40 frames
        for (int i = 0; i < 41; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true,
                canControl = true,
                canFall = true,

            });
        }

        //FAF - 68
        fd.Add(new MoveFrame()
        {
            lastFrame = true,
            canControl = true,
            canFall = true
        });

        Attack exampleProjectileAttack = new Attack(true, hb, fd)
        {
            projectile = hb[0],
            destroyOnHit = true,
            reverseFrames = 6
        };

        return exampleProjectileAttack;
    }

    public static Attack CreateExampleUpBAttack(List<GameObject> hb)
    {
        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 20 frames (10 invincible)
        for (int i = 0; i < 10; i++)
        {
            fd.Add(new MoveFrame()
            {
                startupFrame = true,
                cancelable = false
            });
        }

        for (int i = 0; i < 9; i++)
        {
            fd.Add(new MoveFrame()
            {
                startupFrame = true,
                cancelable = false,
                iFrame = true
            });
        }

        fd.Add(new MoveFrame()
        {
            startupFrame = true,
            cancelable = false,
            iFrame = true,
            userTranslation = new Vector3(0, 0.1F)
        });


        List<bool> activeHitboxes = new List<bool>()
        {
            true,
            false,
            false
        };

        Vector3 userMomentum = new Vector3(0F, 0.225F);

        //Active - 23 frames (2 early, 21 late)
        for (int i = 0; i < 2; i++)
        {
            fd.Add(new MoveFrame()
            {
                hitboxActive = true,
                allHitboxesActive = activeHitboxes,
                userMomentum = userMomentum,
                canControl = true,
                canFall = true,
                cancelable = false
            });
        }

        //Late hit
        activeHitboxes = new List<bool>()
        {
            false,
            true,
            true
        };

        fd.Add(new MoveFrame()
        {
            hitboxActive = true,
            allHitboxesActive = activeHitboxes,
            userMovement = userMomentum,
            canControl = true,
            canFall = true
        });

        //Stops adding momentum after 3 frames
        for (int i = 0; i < 20; i++)
        {
            fd.Add(new MoveFrame()
            {
                hitboxActive = true,
                allHitboxesActive = activeHitboxes,
                canControl = true,
                canFall = true
            });
        }


        //Endlag - 20 frames
        for (int i = 0; i < 20; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true,
                canControl = true,
                canFall = true
            });
        }

        //FAF - 63
        fd.Add(new MoveFrame()
        {
            lastFrame = true,
            canControl = true,
            canFall = true

        });

        Attack UpBAttack = new Attack(true, hb, fd)
        {
            cancelAirMomentum = true,
            reverseFrames = 10,
            groundCancel = true,
            landingLag = 4,
            restoreOnHit = true
        };

        return UpBAttack;

    }

    //Morganis kit
    public static Attack CreateMorganisJabAttack(List<GameObject> hb)
    {

        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 3 frame
        for (int i = 0; i < 3; i++)
        {
            fd.Add(new MoveFrame()
            {
                startupFrame = true
            });
        }

        List<bool> activeHitboxes = new List<bool>()
        {
            true,
            true
        };

        //Active - 2 frames
        for (int i = 0; i < 2; i++)
        {
            fd.Add(new MoveFrame()
            {
                hitboxActive = true,
                allHitboxesActive = activeHitboxes
            });
        }

        //Endlag - 16 frames
        for (int i = 0; i < 16; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true
            });
        }

        //FAF - 21
        fd.Add(new MoveFrame()
        {
            lastFrame = true
        });

        Attack exampleJabAttack = new Attack(hb, fd)
        {
            pierceShield = true
        };

        return exampleJabAttack;
    }

    public static Attack CreateMorganisFTiltAttack(List<GameObject> hb)
    {
        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 8 frames
        for (int i = 0; i < 7; i++)
        {
            fd.Add(new MoveFrame()
            {
                startupFrame = true
            });
        }
        //Step forward on 8th frame of startup
        Vector3 movement = new Vector3(1F, 0F);

        fd.Add(new MoveFrame()
        {
            startupFrame = true,
            userTranslation = movement
        });

        List<bool> activeHitboxes = new List<bool>()
        {
            true,
            true
        };
        //Active - 2 frames
        for (int i = 0; i < 2; i++)
        {
            fd.Add(new MoveFrame()
            {
                hitboxActive = true,
                allHitboxesActive = activeHitboxes
            });
        }

        //Endlag - 18 frames
        for (int i = 0; i < 15; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true
            });
        }

        //FAF - 25
        fd.Add(new MoveFrame()
        {
            lastFrame = true
        });

        return new Attack(hb, fd);
    }

    public static Attack CreateMorganisDTiltAttack(List<GameObject> hb)
    {

        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 3 frames
        for (int i = 0; i < 3; i++)
        {
            fd.Add(new MoveFrame()
            {
                startupFrame = true
            });
        }

        List<bool> activeHitboxes = new List<bool>()
        {
            true,
            true
        };

        //Active - 3 frames
        for (int i = 0; i < 3; i++)
        {
            fd.Add(new MoveFrame()
            {
                hitboxActive = true,
                allHitboxesActive = activeHitboxes
            });
        }
        //Endlag - 17 frames
        for (int i = 0; i < 17; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true
            });
        }

        //FAF - 24
        fd.Add(new MoveFrame()
        {
            lastFrame = true
        });

        return new Attack(hb, fd);
    }

    public static Attack CreateMorganisFAirAttack(List<GameObject> hb)
    {
        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 6 frames
        for (int i = 0; i < 5; i++)
        {
            fd.Add(new MoveFrame()
            {
                startupFrame = true
            });
        }

        fd.Add(new MoveFrame()
        {
            startupFrame = true,
            playSound = true,
            frameSound = AudioContainer.whiff1
        });

        List<bool> activeHitboxes = new List<bool>()
        {
            true,
            false
        };

        //Active - 18 (rehit 5) + 3 frames

        //adds 4 active frames, then a frame with reHit, 3 times
        for (int i = 0; i < 2; i++)
        {

            for (int i2 = 0; i2 < 4; i2++)
            {
                fd.Add(new MoveFrame()
                {
                    hitboxActive = true,
                    allHitboxesActive = activeHitboxes
                });
            }

            fd.Add(new MoveFrame()
            {
                reHit = true,
                playSound = true,
                frameSound = AudioContainer.whiff1
            });
        }

        //Frames before final hit; 5
        for (int i = 0; i < 4; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true
            });
        }

        fd.Add(new MoveFrame()
        {
            endlagFrame = true,
            reHit = true,
            playSound = true,
            frameSound = AudioContainer.whiff1
        });


        activeHitboxes = new List<bool>()
        {
            false,
            true
        };

        //Last hit active for 3 frames
        for (int i = 0; i < 3; i++)
        {
            fd.Add(new MoveFrame()
            {
                hitboxActive = true,
                allHitboxesActive = activeHitboxes
            });
        }


        //Endlag - 11 frames (autocancel 8 frames)
        for (int i = 0; i < 3; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true
            });
        }

        for (int i = 0; i < 8; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true,
                autoCancel = true
            });
        }

        //FAF - 37
        fd.Add(new MoveFrame()
        {
            lastFrame = true
        });

        Attack fAirAttack = new Attack(hb, fd);
        fAirAttack.aerial = true;
        fAirAttack.landingLag = 14;


        return fAirAttack;

    }

    public static Attack CreateMorganisNAirAttack(List<GameObject> hb)
    {

        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 5 frames
        for (int i = 0; i < 5; i++)
        {
            fd.Add(new MoveFrame()
            {
                startupFrame = true
            });
        }

        List<bool> activeHitboxes = new List<bool>()
        {
            true,
            true,
            false,
            false
        };

        //Active - 4, 4 frames (4 in between)
        for (int i = 0; i < 4; i++)
        {
            fd.Add(new MoveFrame()
            {
                hitboxActive = true,
                allHitboxesActive = activeHitboxes
            });
        }
        
        //Between hits - 5 frames
        for (int i = 0; i < 4; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true
            });
        }

        fd.Add(new MoveFrame()
        {
            endlagFrame = true,
            reHit = true
        });

        activeHitboxes = new List<bool>()
        {
            false,
            false,
            true,
            true
        };

        for (int i = 0; i < 4; i++)
        {
            fd.Add(new MoveFrame()
            {
                hitboxActive = true,
                allHitboxesActive = activeHitboxes
            });
        }


        //Endlag - 10 frames (autocancel 5 frames)
        for (int i = 0; i < 5; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true
            });
        }

        for (int i = 0; i < 5; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true,
                autoCancel = true
            });
        }

        //FAF - 42
        fd.Add(new MoveFrame()
        {
            lastFrame = true
        });

        Attack nAirAttack = new Attack(hb, fd);
        nAirAttack.aerial = true;
        nAirAttack.landingLag = 8;


        return nAirAttack;
    }

    public static Attack CreateMorganisDAirAttack(List<GameObject> hb)
    {

        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 4 frames
        for (int i = 0; i < 3; i++)
        {
            fd.Add(new MoveFrame()
            {
                startupFrame = true
            });
        }

        fd.Add(new MoveFrame()
        {
            startupFrame = true,
            playSound = true,
            frameSound = AudioContainer.whiff1
        });

        List<bool> activeHitboxes = new List<bool>()
        {
            true,
            true
        };

        //List of hitbox position changes by frame
        //frame 2
        List<Vector3> frame2Anim = new List<Vector3>()
        {
            new Vector3(0.5F, -0.5F),
            new Vector3(1F, -1F)
        };
        //frame 3
        List<Vector3> frame3Anim = new List<Vector3>()
        {
            new Vector3(0.5F, 0.5F),
            new Vector3(1F, 1F)
        };

        //Active - 3 frames
        fd.Add(new MoveFrame()
        {
            hitboxActive = true,
            allHitboxesActive = activeHitboxes
        });

        fd.Add(new MoveFrame()
        {
            hitboxActive = true,
            allHitboxesActive = activeHitboxes,
            hitboxAnimated = true,
            allHitboxesAnimated = frame2Anim
        });

        fd.Add(new MoveFrame()
        {
            hitboxActive = true,
            allHitboxesActive = activeHitboxes,
            hitboxAnimated = true,
            allHitboxesAnimated = frame3Anim
        });

        //Endlag - 24 frames (autocancel 10 frames)
        for (int i = 0; i < 14; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true
            });
        }

        for (int i = 0; i < 10; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true,
                autoCancel = true
            });
        }

        //FAF - 31
        fd.Add(new MoveFrame()
        {
            lastFrame = true
        });

        Attack dAirAttack = new Attack(hb, fd);
        dAirAttack.aerial = true;
        dAirAttack.landingLag = 15;


        return dAirAttack;
    }

    public static Attack CreateMorganisUAirAttack(List<GameObject> hb)
    {

        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 3 frames
        for (int i = 0; i < 2; i++)
        {
            fd.Add(new MoveFrame()
            {
                startupFrame = true
            });
        }

        fd.Add(new MoveFrame()
        {
            startupFrame = true,
            playSound = true,
            frameSound = AudioContainer.whiff1
        });

        List<bool> activeHitboxes = new List<bool>()
        {
            true,
            true
        };

        //List of hitbox position changes by frame
        //frame 2
        List<Vector3> frame2Anim = new List<Vector3>()
        {
            new Vector3(0.5F, 0.5F),
            new Vector3(1F, 1F)
        };
        //frame 3
        List<Vector3> frame3Anim = new List<Vector3>()
        {
            new Vector3(0.5F, -0.5F),
            new Vector3(1F, -1F)
        };

        fd.Add(new MoveFrame()
        {
            hitboxActive = true,
            allHitboxesActive = activeHitboxes
         });

        fd.Add(new MoveFrame()
        {
            hitboxActive = true,
            allHitboxesActive = activeHitboxes,
            hitboxAnimated = true,
            allHitboxesAnimated = frame2Anim
        });

        fd.Add(new MoveFrame()
        {
            hitboxActive = true,
            allHitboxesActive = activeHitboxes,
            hitboxAnimated = true,
            allHitboxesAnimated = frame3Anim
        });

        //Endlag - 16 frames (autocancel 10 frames)
        for (int i = 0; i < 6; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true
            });
        }

        for (int i = 0; i < 10; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true,
                autoCancel = true
            });
        }

        //FAF - 22
        fd.Add(new MoveFrame()
        {
            lastFrame = true
        });

        Attack uAirAttack = new Attack(hb, fd);
        uAirAttack.aerial = true;
        uAirAttack.landingLag = 12;


        return uAirAttack;
    }

    public static Attack CreateMorganisBAirAttack(List<GameObject> hb)
    {

        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 14 frames

        for (int i = 0; i < 13; i++)
        {
            fd.Add(new MoveFrame()
            {
                startupFrame = true,
            });
        }
        fd.Add(new MoveFrame()
        {
            startupFrame = true,
            playSound = true,
            frameSound = AudioContainer.swordWhiff2
        });


        List<bool> activeHitboxes = new List<bool>()
        {
            true,
            true
        };

        //Active - 3 frames
        for (int i = 0; i < 3; i++)
        {
            fd.Add(new MoveFrame()
            {
                hitboxActive = true,
                allHitboxesActive = activeHitboxes,
            });
        }

        //Endlag - 35 frames (autocancel 15 frames)
        for (int i = 0; i < 20; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true
            });
        }

        for (int i = 0; i < 15; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true,
                autoCancel = true
            });
        }

        //FAF - 52
        fd.Add(new MoveFrame()
        {
            lastFrame = true
        });

        Attack bAirAttack = new Attack(hb, fd);
        bAirAttack.aerial = true;
        bAirAttack.landingLag = 18;


        return bAirAttack;
    }

    public static Attack CreateMorganisProjectileAttack(List<GameObject> hb)
    {
        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 22 frames
        for (int i = 0; i < 22; i++)
        {
            fd.Add(new MoveFrame()
            {
                startupFrame = true,
                canControl = true,
                canFall = true
            });
        }

        fd.Add(new MoveFrame()
        {
            spawnProjectile = true,
            canControl = true,
            canFall = true,
            playSound = true,
            frameSound = AudioContainer.whiff1
        });

        //Endlag - 18 frames
        for (int i = 0; i < 18; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true,
                canControl = true,
                canFall = true
            });
        }

        //FAF - 36
        fd.Add(new MoveFrame()
        {
            lastFrame = true,
            canControl = true,
            canFall = true
        });

        Attack exampleProjectileAttack = new Attack(true, hb, fd)
        {
            projectile = hb[0],
            destroyOnHit = true,
            reverseFrames = 6,
        };

        return exampleProjectileAttack;
    }

    public static Attack CreateMorganisSideBAttack(List<GameObject> hb)
    {
        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 15 frames
        for (int i = 0; i < 14; i++)
        {
            fd.Add(new MoveFrame()
            {
                startupFrame = true
            });
        }

        fd.Add(new MoveFrame()
        {
            startupFrame = true,
            playSound = true,
            frameSound = AudioContainer.whiff1
        });

        List<bool> activeHitboxes = new List<bool>()
        {
            true
        };

        Vector3 movement = new Vector3(0.8F, 0);

        //Active - 16 frames (10 early, 4 late)
        fd.Add(new MoveFrame()
        {
            hitboxActive = true,
            allHitboxesActive = activeHitboxes
        });

        for (int i = 0; i < 9; i++)
        {
            fd.Add(new MoveFrame()
            {
                hitboxActive = true,
                allHitboxesActive = activeHitboxes,
                userTranslation = movement,
                setMomentum = true
            });
        }


        //Endlag - 16 frames
        for (int i = 0; i < 16; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true
            });
        }

        //FAF - 43
        fd.Add(new MoveFrame()
        {
            lastFrame = true,
            canControl = true,
            canFall = true

        });

        Attack sideBAttack = new Attack(true, hb, fd)
        {
            cancelAirMomentum = true,
            reverseFrames = 5,
            endlagOnHit = true,
            restoreOnHit = true
        };

        return sideBAttack;
    }

    public static Attack CreateMorganisUpBAttack(List<GameObject> hb)
    {
        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 3 frames
        for (int i = 0; i < 2; i++)
        {
            fd.Add(new MoveFrame()
            {
                startupFrame = true
            });
        }

        fd.Add(new MoveFrame()
        {
            startupFrame = true,
            playSound = true,
            frameSound = AudioContainer.swordDraw2
        });

        List<bool> activeHitboxes = new List<bool>()
        {
            true,
            false
        };

        Vector3 userMovementEarly = new Vector3(0.4F, 1.8F);
        Vector3 userMovementLate = new Vector3(0.2F, 0.6F);

        //Active - 8 frames (4 early, 4 late)
        fd.Add(new MoveFrame()
        {
            hitboxActive = true,
            allHitboxesActive = activeHitboxes
        });

        for (int i = 0; i < 3; i++)
        {
            fd.Add(new MoveFrame()
            {
                hitboxActive = true,
                allHitboxesActive = activeHitboxes,
                userTranslation = userMovementEarly,
            });
        }

        //Late hit
        activeHitboxes = new List<bool>()
        {
            false,
            true
        };

        fd.Add(new MoveFrame()
        {
            hitboxActive = true,
            allHitboxesActive = activeHitboxes,
            userTranslation = userMovementEarly,
            canControl = true,
        });

        for (int i = 0; i < 3; i++)
        {
            fd.Add(new MoveFrame()
            {
                hitboxActive = true,
                allHitboxesActive = activeHitboxes,
                userTranslation = userMovementLate,
                canControl = true,
            });
        }


        //Endlag - 10 frames (10, 3 falling)
        for (int i = 0; i < 7; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true,
                canControl = true
            });
        }

        for (int i = 0; i < 3; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true,
                canControl = true,
                canFall = true
            });
        }

        //FAF - 19
        fd.Add(new MoveFrame()
        {
            lastFrame = true,
            canControl = true,
            canFall = true

        });

        Attack UpBAttack = new Attack(true, hb, fd)
        {
            cancelAirMomentum = true,
            reverseFrames = 4,
            restoreOnHit = true
        };

        return UpBAttack;

    }

    public static Attack CreateAerialMorganisUpBAttack(List<GameObject> hb)
    {
        List<MoveFrame> fd = new List<MoveFrame>();

        //Startup - 3 frames
        for (int i = 0; i < 2; i++)
        {
            fd.Add(new MoveFrame()
            {
                startupFrame = true
            });
        }

        fd.Add(new MoveFrame()
        {
            startupFrame = true,
            playSound = true,
            frameSound = AudioContainer.swordDraw2
        });

        List<bool> activeHitboxes = new List<bool>()
        {
            true,
            false
        };

        Vector3 userMovementEarly = new Vector3(0.5F, 2.0F);
        Vector3 userMovementLate = new Vector3(0.3F, 0.6F);

        //Active - 7 frames (3 early, 4 late)
        for (int i = 0; i < 3; i++)
        {
            fd.Add(new MoveFrame()
            {
                hitboxActive = true,
                allHitboxesActive = activeHitboxes,
                userTranslation = userMovementEarly,
            });
        }

        //Late hit
        activeHitboxes = new List<bool>()
        {
            false,
            true
        };

        fd.Add(new MoveFrame()
        {
            hitboxActive = true,
            allHitboxesActive = activeHitboxes,
            userTranslation = userMovementEarly,
            canControl = true,
        });

        for (int i = 0; i < 3; i++)
        {
            fd.Add(new MoveFrame()
            {
                hitboxActive = true,
                allHitboxesActive = activeHitboxes,
                userTranslation = userMovementLate,
                canControl = true,
            });
        }


        //Endlag - 10 frames (10, 3 falling)
        for (int i = 0; i < 7; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true,
                canControl = true
            });
        }

        for (int i = 0; i < 3; i++)
        {
            fd.Add(new MoveFrame()
            {
                endlagFrame = true,
                canControl = true,
                canFall = true
            });
        }

        //FAF - 19
        fd.Add(new MoveFrame()
        {
            lastFrame = true,
            canControl = true,
            canFall = true

        });

        Attack UpBAttack = new Attack(true, hb, fd)
        {
            cancelAirMomentum = true,
            reverseFrames = 4,
            restoreOnHit = true
        };

        return UpBAttack;

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