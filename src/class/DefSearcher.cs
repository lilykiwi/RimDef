using SimpleSearch;
using System.Collections.Generic;
using System.Linq;

namespace RimDef
{
    class DefSearcher : Searcher
    {
        private List<Def> Defs { get; set; }

        public DefSearcher(List<Def> defs)
        {
            //Dummy up some data here...
            //Defs = new List<Def>();
            this.Defs = defs;
        }

        public override IEnumerable<SearchResult> Search(string searchTerm)
        {
            var result = Defs.Where(
                    p => p.label.ToLower().Contains(searchTerm.ToLower()) /*|| p.description.ToLower().Contains(searchTerm.ToLower())*/
                )
                .Select(
                    p =>
                        new SearchResult
                        {
                            //Mod = p.modName,
                            //Label = p.label,
                            //Description = p.description
                            Definition = p
                        }
                )
                .ToList();

            return result;
        }
    }
}
