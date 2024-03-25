using System.Collections.Generic;

namespace RimDef
{
  class RecipeDef : Def
  {
    public List<string[]> ingredients = new List<string[]>();

    public void AddIngredients(string[] row)
    {
      ingredients.Add(row);
    }

    public string research;
    public string skill;
    public string work;

    public RecipeDef(
        string research,
        string skill,
        string work,
        Mod mod,
        string defType,
        string defName,
        string label,
        string description,
        string texture,
        string xml,
        string file
    )
        : base(mod, defType, defName, label, description, texture, xml, file)
    {
      this.research = research;
      this.skill = skill;
      this.work = work;
    }
  }
}
