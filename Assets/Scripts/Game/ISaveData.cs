public interface ISaveData
{
    public IDataStructForSave GetSaveData();
    public void SetLoadData(IDataStructForSave saveData);
    public void SetDefaultData();
}

public interface IDataStructForSave
{
        
}