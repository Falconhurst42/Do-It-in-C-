using Microsoft.EntityFrameworkCore;

namespace DoItInCpp.Models
{
    public static class SeedDatabase
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DoItInCppContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<DoItInCppContext>>()))
            {
                if (context == null || context.Snippets == null)
                {
                    throw new ArgumentNullException("Null DoItInCppContext");
                }

                // Look for any Snippets.
                if (context.Snippets.Any())
                {
                    return;   // DB has been seeded
                }

                /*context.LanguageVersions.AddRange(
                    new LanguageVersion{Year_XX = 98},
                    new LanguageVersion{Year_XX = 03},
                    new LanguageVersion{Year_XX = 11},
                    new LanguageVersion{Year_XX = 14},
                    new LanguageVersion{Year_XX = 17},
                    new LanguageVersion{Year_XX = 20}
                );*/

                context.Snippets.AddRange(
                    new Snippet
                    {
                        Name = "Sort",
                        Description = "Sort the contents of a container or array using std::sort",
                        Code = "// create the container\nstd::vector<int> myvector = {32,71,12,45,26,53,80,33};\n"
                                + "// myvector contains: 32 71 12 45 26 53 80 33\n"
                                + "\n"
                                + "// sort the container\n"
                                + "std::sort(myvector.begin(), myvector.end());\n"
                                + "// myvector now contains: (12 26 32 33 45 53 71 80)",
                        Documentation = "The `std::sort` function sorts the elements in the range [start_iterator, end_iterator) in ascending order."
                                + "On line 2, we create the container we wish to sort, in this case, a `vector` containing a series of 8 unsorted `int`."
                                + "On line 6, we sort the container using the `std::sort` function. To pass the `vector` to `sort`, we use its `.begin()` and `.end()` methods to get iterators referring to the beginning and end positions of the `vector`. The `sort` function sorts everything between these iterators, in this case, the entire `vector`.",
                        Created = DateTime.Parse("3-10-2022"),
                        LastUpdated = DateTime.Parse("4-12-2022 5:00pm"),
                        VersionID = 98
                    }
                );
                context.SaveChanges();
            }
        }
    }
}