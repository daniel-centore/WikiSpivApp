using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using WikiClientLibrary.Client;
using WikiClientLibrary.Generators;
using WikiClientLibrary.Pages;
using WikiClientLibrary.Pages.Queries;
using WikiClientLibrary.Sites;
using WikiSpiv.Extras;
using WikiSpiv.Models;

namespace WikiSpiv.Data
{
    public class WikiSpivDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        static readonly ObservableRangeCollection<WikiSpivItem> wikiSpivItems = new ObservableRangeCollection<WikiSpivItem>();
        public static ObservableRangeCollection<WikiSpivItem> WikiSpivItems { get { return wikiSpivItems; } }

        
        public WikiSpivDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(WikiSpivItem).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(WikiSpivItem)).ConfigureAwait(false);
                }

                //Console.WriteLine(Database.)


                //await Database.DeleteAllAsync<WikiSpivItem>();  // TODO: Remove

                List<WikiSpivItem> dbItems = await Database.Table<WikiSpivItem>().ToListAsync();
                await UpdateItems(
                    dbItems,
                    true,  // replaceExisting
                    false  // updateDb
                );
                //wikiSpivItems.Clear();
                //foreach (WikiSpivItem wsi in dbItems)
                //{
                //    wikiSpivItems.Add(wsi);
                //}

                await FetchMediaWikiPages();


                //for (int i = 0; i < 10; ++i)
                //{
                //    WikiSpivItem fakeItem = new WikiSpivItem();
                //    fakeItem.Name = "Potato " + DateTime.Now;
                //    await SaveItemAsync(fakeItem);

                //    await Task.Delay(1000);
                //}

                //for (int i = 0; i < wikiSpivItems.Count; ++i)
                //{
                //    WikiSpivItem wsi = wikiSpivItems[i];
                //    if (wsi.Content == null || wsi.Content.Trim().Length == 0)
                //    {
                //        wsi.Content = "I love potatoes " + DateTime.Now;
                //    }

                //    await SaveItemAsync(wsi);

                //    await Task.Delay(1000);
                //}


                initialized = true;
            }
        }

        private static async Task FetchMediaWikiPages()
        {
            // <PackageReference Include="CXuesong.MW.WikiClientLibrary" Version="0.7.4" ExcludeAssets="CXuesong.AsyncEnumerableExtensions" />
            var client = new WikiClient
            {
                ClientUserAgent = "WSApp/1.0"
            };
            //// You can create multiple WikiSite instances on the same WikiClient to share the state.
            var site = new WikiSite(client, "https://www.wikispiv.com/api.php");
            //// Wait for initialization to complete.
            await site.Initialization;

            //// Throws error if any.
            AllPagesGenerator allPagesGenerator = new AllPagesGenerator(site)
            {
                PaginationSize = 500
            };
            var results = await allPagesGenerator.EnumItemsAsync().ToListAsync().ConfigureAwait(false);
            Console.WriteLine("Got " + results.Count + " results");

            List<WikiSpivItem> wsItems = new List<WikiSpivItem>();
            foreach (WikiPageStub stub in results)
            {
                String unparsedTitle = stub.Title;
                //Console.WriteLine(unparsedTitle);
                //String content = page.Content;
                String type = "song";
                String title = unparsedTitle;
                if (unparsedTitle.Contains(":"))
                {
                    string[] titleSplit = unparsedTitle.Split(':');
                    type = titleSplit[0];
                    title = titleSplit[1];
                }
                WikiSpivItem wikiSpivItem = new WikiSpivItem(0, type, title, "");
                wsItems.Add(wikiSpivItem);
                //await SaveItemAsync(wikiSpivItem);
            }


            // TODO: Don't override the DB content with these titles if we already have it!!!
            //       Only use the titles for adding / removing the title information
            //wikiSpivItems.ReplaceRange(wsItems);
            await UpdateItems(wsItems, false);




            allPagesGenerator = new AllPagesGenerator(site);
            var provider = WikiPageQueryProvider.FromOptions(PageQueryOptions.FetchContent);
            var pageResults = await allPagesGenerator.EnumPagesAsync(provider).ToListAsync().ConfigureAwait(false);

            wsItems = new List<WikiSpivItem>();
            foreach (WikiPage page in pageResults)
            {
                String unparsedTitle = page.Title;
                //Console.WriteLine(unparsedTitle);
                String content = page.Content;
                String type = "song";
                String title = unparsedTitle;
                if (content == null)
                {
                    content = "";
                }
                if (unparsedTitle.Contains(":"))
                {
                    string[] titleSplit = unparsedTitle.Split(':');
                    type = titleSplit[0];
                    title = titleSplit[1];
                }
                WikiSpivItem wikiSpivItem = new WikiSpivItem(0, type, title, "hey" + content);
                wsItems.Add(wikiSpivItem);
                //await SaveItemAsync(wikiSpivItem);
            }
            await UpdateItems(wsItems, true);
        }

        private static long GetUnixTime()
        {
            TimeSpan timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
            return (long)timeSpan.TotalSeconds;
        }

        //public Task<List<WikiSpivItem>> GetItemsAsync()
        //{
        //    return Database.Table<WikiSpivItem>().ToListAsync();
        //}

        //public Task<List<WikiSpivItem>> GetItemsNotDoneAsync()
        //{
        //    return Database.QueryAsync<WikiSpivItem>("SELECT * FROM [WikiSpivItem] WHERE [Done] = 0");
        //}

        //public Task<WikiSpivItem> GetItemAsync(int id)
        //{
        //    return Database.Table<WikiSpivItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="replaceExisting">If one of the items already exists, should we replace its contents?</param>
        /// <returns></returns>
        private static async Task UpdateItems(List<WikiSpivItem> items, bool replaceExisting, bool updateDb = true)
        {
            items.Sort((a, b) => a.Name.CompareTo(b.Name));

            List<WikiSpivItem> newItems = new List<WikiSpivItem>();

            // This will only include items which are present in the new items list
            foreach (WikiSpivItem item in items)
            {
                WikiSpivItem existing = wikiSpivItems.FirstOrDefault(i => i.Name == item.Name && i.Type == item.Type);
                if (existing != null && !replaceExisting)
                    newItems.Add(existing);
                else
                    newItems.Add(item);
            }

            wikiSpivItems.ReplaceRange(newItems);

            if (updateDb)
            {
                await Database.DeleteAllAsync<WikiSpivItem>();
                foreach (WikiSpivItem item in wikiSpivItems)
                {
                    await SaveItemToDbAsync(item);
                }
            }
        }

        //private static async Task SaveItems(List<WikiSpivItem> items)
        //{
        //    wikiSpivItems.ReplaceRange(items);
        //}

        private static async Task SaveItemToDbAsync(WikiSpivItem item)
        {
            bool update;
            if (item.ID >= 0)
                update = true;
            else
            {
                int count = await Database.Table<WikiSpivItem>().CountAsync();
                if (count == 0)
                    update = false;
                else
                {
                    WikiSpivItem existingDbItem = await Database.Table<WikiSpivItem>()
                        .FirstOrDefaultAsync(i => i.GUID == item.GUID);
                    if (existingDbItem == null)
                        update = false;
                    else
                    {
                        item.ID = existingDbItem.ID;
                        update = true;
                    }
                }
            }

            if (update)
                await Database.UpdateAsync(item);
            else
            {
                await Database.InsertAsync(item);
                WikiSpivItem insertedItem = await Database.Table<WikiSpivItem>().FirstAsync(i => i.GUID == item.GUID);
                item.ID = insertedItem.ID;
            }

        }

        //public Task<int> DeleteItemAsync(WikiSpivItem item)
        //{
        //    return Database.DeleteAsync(item);
        //}

    }
}
