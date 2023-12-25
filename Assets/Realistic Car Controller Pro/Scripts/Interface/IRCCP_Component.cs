/// <summary>
/// Interface for all modular components.
/// </summary>
public interface IRCCP_Component {

    /// <summary>
    /// Initializes and registers the target component.
    /// </summary>
    /// <param name="connectedCarController"></param>
    public void Initialize(RCCP_CarController connectedCarController);

}
