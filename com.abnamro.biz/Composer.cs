using System;
using System.Threading.Tasks;

namespace com.abnamro.biz
{
    internal static class Composer
    {
        static internal TComposition SelectAndProject<TSelectionOne, TSelectionTwo, TComposition>(Func<TSelectionOne> selectOne, Func<TSelectionTwo> selectTwo, Func<TSelectionOne, TSelectionTwo, TComposition> project)
        {
            if (selectOne == default(Func<TSelectionOne>)) throw new ArgumentNullException(nameof(selectOne));
            if (selectTwo == default(Func<TSelectionTwo>)) throw new ArgumentNullException(nameof(selectTwo));
            if (project == default(Func<TSelectionOne, TSelectionTwo, TComposition>)) throw new ArgumentNullException(nameof(project));

            return project(selectOne(), selectTwo());
        }

        async static internal Task<TComposition> SelectAndProjectAsync<TSelectionOne, TSelectionTwo, TComposition>(Task<TSelectionOne> taskOne, Task<TSelectionTwo> taskTwo, Func<TSelectionOne, TSelectionTwo, TComposition> project)
        {
            if (taskOne == default(Task<TSelectionOne>)) throw new ArgumentNullException(nameof(taskOne));
            if (taskTwo == default(Task<TSelectionTwo>)) throw new ArgumentNullException(nameof(taskTwo));
            if (project == default(Func<TSelectionOne, TSelectionTwo, TComposition>)) throw new ArgumentNullException(nameof(project));

            return await Task.WhenAll(new Task[] { taskOne, taskTwo }).ContinueWith(task => project(taskOne.Result, taskTwo.Result));
        }

        static internal TComposition SelectAndProject<TSelectionOne, TSelectionTwo, TSelectionThree, TComposition>(Func<TSelectionOne> selectOne, Func<TSelectionTwo> selectTwo, Func<TSelectionThree> selectThree, Func<TSelectionOne, TSelectionTwo, TSelectionThree, TComposition> project)
        {
            if (selectOne == default(Func<TSelectionOne>)) throw new ArgumentNullException(nameof(selectOne));
            if (selectTwo == default(Func<TSelectionTwo>)) throw new ArgumentNullException(nameof(selectTwo));
            if (selectThree == default(Func<TSelectionThree>)) throw new ArgumentNullException(nameof(selectThree));
            if (project == default(Func<TSelectionOne, TSelectionTwo, TSelectionThree, TComposition>)) throw new ArgumentNullException(nameof(project));

            return project(selectOne(), selectTwo(), selectThree());
        }

        async static internal Task<TComposition> SelectAndProjectAsync<TSelectionOne, TSelectionTwo, TSelectionThree, TComposition>(Task<TSelectionOne> taskOne, Task<TSelectionTwo> taskTwo, Task<TSelectionThree> taskThree, Func<TSelectionOne, TSelectionTwo, TSelectionThree, TComposition> project)
        {
            if (taskOne == default(Task<TSelectionOne>)) throw new ArgumentNullException(nameof(taskOne));
            if (taskTwo == default(Task<TSelectionTwo>)) throw new ArgumentNullException(nameof(taskTwo));
            if (taskThree == default(Task<TSelectionThree>)) throw new ArgumentNullException(nameof(taskThree));
            if (project == default(Func<TSelectionOne, TSelectionTwo, TSelectionThree, TComposition>)) throw new ArgumentNullException(nameof(project));

            return await Task.WhenAll(new Task[] { taskOne, taskTwo, taskThree }).ContinueWith(task => project(taskOne.Result, taskTwo.Result, taskThree.Result));
        }

        async static internal Task<TComposition> SelectAndProjectAsync<TSelectionOne, TSelectionTwo, TSelectionThree, TSelectionFour, TComposition>(Task<TSelectionOne> taskOne, Task<TSelectionTwo> taskTwo, Task<TSelectionThree> taskThree, Task<TSelectionFour> taskFour, Func<TSelectionOne, TSelectionTwo, TSelectionThree, TSelectionFour, TComposition> project)
        {
            if (taskOne == default(Task<TSelectionOne>)) throw new ArgumentNullException(nameof(taskOne));
            if (taskTwo == default(Task<TSelectionTwo>)) throw new ArgumentNullException(nameof(taskTwo));
            if (taskThree == default(Task<TSelectionThree>)) throw new ArgumentNullException(nameof(taskThree));
            if (taskFour == default(Task<TSelectionFour>)) throw new ArgumentNullException(nameof(taskFour));
            if (project == default(Func<TSelectionOne, TSelectionTwo, TSelectionThree, TSelectionFour, TComposition>)) throw new ArgumentNullException(nameof(project));

            return await Task.WhenAll(new Task[] { taskOne, taskTwo, taskThree }).ContinueWith(task => project(taskOne.Result, taskTwo.Result, taskThree.Result, taskFour.Result));
        }

        static internal TComposition SelectAndProject<TSelectionOne, TSelectionTwo, TSelectionThree, TSelectionFour, TComposition>(Func<TSelectionOne> selectOne, Func<TSelectionTwo> selectTwo, Func<TSelectionThree> selectThree, Func<TSelectionFour> selectFour, Func<TSelectionOne, TSelectionTwo, TSelectionThree, TSelectionFour, TComposition> project)
        {
            if (selectOne == default(Func<TSelectionOne>)) throw new ArgumentNullException(nameof(selectOne));
            if (selectTwo == default(Func<TSelectionTwo>)) throw new ArgumentNullException(nameof(selectTwo));
            if (selectThree == default(Func<TSelectionThree>)) throw new ArgumentNullException(nameof(selectThree));
            if (selectFour == default(Func<TSelectionFour>)) throw new ArgumentNullException(nameof(selectFour));
            if (project == default(Func<TSelectionOne, TSelectionTwo, TSelectionThree, TSelectionFour, TComposition>)) throw new ArgumentNullException(nameof(project));

            return project(selectOne(), selectTwo(), selectThree(), selectFour());
        }

    }
}
