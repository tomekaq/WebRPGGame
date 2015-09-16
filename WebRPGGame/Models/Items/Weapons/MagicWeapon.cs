
namespace ModelingObjectTask.Items
{
    public class MagicWeapon : Weapons
    {
        public override int Attack { get; set; }

        public MagicWeapon()
        {
            Attack = 1;
        }
    }
}
