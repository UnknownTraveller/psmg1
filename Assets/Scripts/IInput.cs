// shamelessly copied from our main project (all these are still by me).
public interface IInput
{
    float Horizontal { get; }
    float Vertical { get; }
    bool Interact { get; }
}