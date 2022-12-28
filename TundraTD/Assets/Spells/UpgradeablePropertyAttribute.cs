using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Spells
{
    public abstract class UpgradeablePropertyAttribute : Attribute
    {
        public BasicElement ConfirmableElements { get; set; }

        public abstract bool Upgrade(BasicElement element);
    }
}
