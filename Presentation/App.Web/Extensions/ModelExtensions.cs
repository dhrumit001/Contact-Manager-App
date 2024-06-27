using App.Core;
using App.Web.Models;
using System.Linq;

namespace App.Web.Extensions
{
    public static class ModelExtensions
    {
        /// <summary>
        /// Prepare passed list model to display in a grid
        /// </summary>
        /// <typeparam name="TListModel">List model type</typeparam>
        /// <typeparam name="TModel">Model type</typeparam>
        /// <typeparam name="TObject">Object type</typeparam>
        /// <param name="listModel">List model</param>
        /// <param name="searchModel">Search model</param>
        /// <param name="objectList">Paged list of objects</param>
        /// <param name="dataFillFunction">Function to populate model data</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the list model
        /// </returns>
        public static TListModel PrepareToGrid<TListModel, TModel, TObject>(this TListModel listModel,
            BaseSearchModel searchModel, IPagedList<TObject> objectList, Func<IEnumerable<TModel>> dataFillFunction)
            where TListModel : BasePagedListModel<TModel>
        {
            if (listModel == null)
                throw new ArgumentNullException(nameof(listModel));

            listModel.Data = (dataFillFunction?.Invoke()).ToList();
            listModel.Draw = searchModel?.Draw;
            listModel.RecordsTotal = objectList?.TotalCount ?? 0;
            listModel.RecordsFiltered = objectList?.TotalCount ?? 0;

            return listModel;
        }
    }
}
