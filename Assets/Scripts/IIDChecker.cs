

using Cysharp.Threading.Tasks;

public interface IIdChecker
{
    UniTask<bool> CheckId(int id);
}
