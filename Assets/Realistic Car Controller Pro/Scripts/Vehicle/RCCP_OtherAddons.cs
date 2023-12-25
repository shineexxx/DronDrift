//----------------------------------------------
//        Realistic Car Controller Pro
//
// Copyright © 2014 - 2023 BoneCracker Games
// https://www.bonecrackergames.com
// Ekrem Bugra Ozdoganlar
//
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Other addons belongs to the vehicle, such as nos, dashboard, interior, cameras, exhaust, AI, recorder, attacher, etc...
/// </summary>
public class RCCP_OtherAddons : RCCP_Component {

    //  Nos.
    public RCCP_Nos Nos {

        get {

            return _nos;

        }
        set {

            _nos = value;

        }

    }

    //  Visual dashboard.
    public RCCP_Visual_Dashboard Dashboard {

        get {

            return _dashboard;

        }
        set {

            _dashboard = value;

        }

    }

    // Hood and wheel cameras.
    public RCCP_Exterior_Cameras ExteriorCameras {

        get {

            return _exteriorCameras;

        }
        set {

            _exteriorCameras = value;

        }

    }

    //  Exhausts.
    public RCCP_Exhausts Exhausts {

        get {

            return _exhausts;

        }
        set {

            _exhausts = value;

        }

    }

    //  AI.
    public RCCP_AI AI {

        get {

            return _AI;

        }
        set {

            _AI = value;

        }

    }

    //  Recorder.
    public RCCP_Recorder Recorder {

        get {

            return _recorder;

        }
        set {

            _recorder = value;

        }

    }

    //  Trail attacher.
    public RCCP_TrailerAttacher TrailAttacher {

        get {

            return _trailerAttacher;

        }
        set {

            _trailerAttacher = value;

        }

    }

    //  Limiter.
    public RCCP_Limiter Limiter {

        get {

            return _limiter;

        }
        set {

            _limiter = value;

        }

    }

    private RCCP_Nos _nos;
    private RCCP_Visual_Dashboard _dashboard;
    private RCCP_Exterior_Cameras _exteriorCameras;
    private RCCP_Exhausts _exhausts;
    private RCCP_AI _AI;
    private RCCP_Recorder _recorder;
    private RCCP_TrailerAttacher _trailerAttacher;
    private RCCP_Limiter _limiter;

}
