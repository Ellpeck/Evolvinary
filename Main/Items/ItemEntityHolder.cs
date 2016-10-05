using System;
using Evolvinary.Main.Worlds.Entities;

namespace Evolvinary.Main.Items{
    public class ItemEntityHolder : Item{
        public Type Entity;

        public ItemEntityHolder(string title, string description, Type entity) : base(title, description){
            this.Entity = entity;
        }

        public EntityPlaceable createEntity(){
            return Activator.CreateInstance(this.Entity) as EntityPlaceable;
        }
    }
}