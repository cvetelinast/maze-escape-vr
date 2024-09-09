using Unidux;

public partial class UniduxState : StateBase {

    public Payload payload { get; set; }

    public UniduxState()
    {
        this.payload = new Payload();
    }

    public UniduxState(Payload payload)
    {
        this.payload = payload;
    }

    public override object Clone()
    {
        return this;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || obj.GetType() != this.GetType())
        {
            return false;
        }

        var other = (UniduxState)obj;
        return this.payload == other.payload;
    }

    public override int GetHashCode()
    {
        return this.payload.GetHashCode();
    }

    public override string ToString()
    {
        return string.Format("UniduxState(payload={0})", this.payload);
    }

}

public class InitialState : UniduxState {
    public InitialState() : base(new Payload()) { }
}

public class MenuSceneState : UniduxState {
    public MenuSceneState(Payload payload) : base(payload) { }
}

public class LoadingState : UniduxState {
    public LoadingState(Payload payload) : base(payload) { }
}

public class GameSceneState : UniduxState {
    public GameSceneState(Payload payload) : base(payload) { }
}
