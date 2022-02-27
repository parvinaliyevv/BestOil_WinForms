using homework.Database;
using homework.Views;
using System;

namespace homework.Presents
{
    class MainPresenter
    {
        private readonly IMainView mainView;

        public MainPresenter(IMainView mainView)
        {
            this.mainView = mainView;

            mainView.GetFuels += MainViewLoad;
        }

        private void MainViewLoad(object sender, EventArgs e) => mainView.Fuels = DB.Fuels;
    }
}
