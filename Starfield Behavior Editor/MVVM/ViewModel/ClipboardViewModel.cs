using Nodify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BehaviorEditor.MVVM.ViewModel
{
    public static class ClipboardViewModel 
    {
        public static NodeViewModel? CopiedNodeViewModel { get; private set; }

        public static PropertySheetViewModel? CopiedPropertySheetViewModel { get; private set; }



        public static void Paste(NodeViewModel nodeViewModel)
        {
            if (CopiedPropertySheetViewModel == null) return;
            nodeViewModel.AppendPropertySheet(new PropertySheetViewModel(CopiedPropertySheetViewModel));
        }

        public static void Paste(EditorViewModel editorViewModel)
        {
            if (CopiedNodeViewModel == null) return;
            editorViewModel.AppendNodeViewModel(new NodeViewModel(CopiedNodeViewModel), true);

        }
        public static void Paste(LinkViewModel linkViewModel)
        {
            if (CopiedPropertySheetViewModel == null) return;
            linkViewModel.AppendPropertySheet(new PropertySheetViewModel(CopiedPropertySheetViewModel));
        }

        public static void Paste(PropertySheetViewModel propertySheetViewModel)
        {
            if (CopiedPropertySheetViewModel == null) return;
            foreach(var rowVM in CopiedPropertySheetViewModel.RowViewModels.ToList()) { propertySheetViewModel.AppendRow(new RowViewModel(rowVM));  }
        }

        public static void Copy(PropertySheetViewModel propertySheetViewModel)
        {
            CopiedPropertySheetViewModel = propertySheetViewModel;
        }

        public static void Copy(NodeViewModel nodeViewModel)
        {
            CopiedNodeViewModel = nodeViewModel;
        }

    }
}
