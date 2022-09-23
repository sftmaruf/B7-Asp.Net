using Autofac;
using Library.Areas.Admin.Models;
using AdminBookListModel = Library.Areas.Admin.Models.BookListModel;
using MemberBookListModel = Library.Areas.Member.Models.BookListModel;

namespace Library
{
    public class LibraryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BookCreateModel>().AsSelf();

            builder.RegisterType<ReaderCreateModel>().AsSelf();
            
            builder.RegisterType<AdminBookListModel>().AsSelf();
            
            builder.RegisterType<MemberBookListModel>().AsSelf();

            builder.RegisterType<ReaderListModel>().AsSelf();

            base.Load(builder);
        }
    }
}
