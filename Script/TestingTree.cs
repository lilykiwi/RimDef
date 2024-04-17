using Godot;

public partial class TestingTree : Tree
{

  public override void _Ready()
  {
    base._Ready();

    TreeItem BiomeDef = AddTreeObject("BiomeDef");

    TreeItem defName = AddTreeObject(BiomeDef, "defName");
    TreeItem defNameVal = AddTreeObject(defName, "BorealForest");

    TreeItem label = AddTreeObject(BiomeDef, "label");
    TreeItem labelVal = AddTreeObject(label, "boreal forest");

    TreeItem description = AddTreeObject(BiomeDef, "description");
    TreeItem descriptionVal = AddTreeObject(description, "Forests of coniferous trees. Despite the harsh winters, boreal forests sustain a diverse population of small and large animals, and have warm summers.");

    TreeItem workerClass = AddTreeObject(BiomeDef, "workerClass");
    TreeItem workerClassVal = AddTreeObject(workerClass, "BiomeWorker_BorealForest");

    TreeItem allowFarmingCamps = AddTreeObject(BiomeDef, "allowFarmingCamps");
    TreeItem allowFarmingCampsVal = AddTreeObject(allowFarmingCamps, "false");

    TreeItem animalDensity = AddTreeObject(BiomeDef, "animalDensity");
    TreeItem animalDensitVal = AddTreeObject(animalDensity, "2.8");

    TreeItem plantDensity = AddTreeObject(BiomeDef, "plantDensity");
    TreeItem plantDensityVal = AddTreeObject(plantDensity, "0.40");

    TreeItem settlementSelectionWeight = AddTreeObject(BiomeDef, "settlementSelectionWeight");
    TreeItem settlementSelectionWeightVal = AddTreeObject(settlementSelectionWeight, "0.9");

    TreeItem movementDifficulty = AddTreeObject(BiomeDef, "movementDifficulty");
    TreeItem movementDifficultyVal = AddTreeObject(movementDifficulty, "1");
  }

  public TreeItem AddTreeObject(string name)
  {
    TreeItem item = CreateItem();
    item.SetText(0, name);

    return item;
  }

  public TreeItem AddTreeObject(TreeItem target, string name)
  {
    TreeItem item = CreateItem(target);
    item.SetText(0, name);

    return item;
  }
}
