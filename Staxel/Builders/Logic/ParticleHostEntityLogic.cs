﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plukit.Base;
using Staxel;
using Staxel.Core;
using Staxel.Effects;
using Staxel.Logic;
using Staxel.Particles;
using Staxel.Tiles;

namespace NimbusFox.FoxCore.Staxel.Builders.Logic {
    public class ParticleHostEntityLogic : EntityLogic {
        private readonly Entity _entity;
        private readonly Blob _blob;
        private Vector3D _location;
        public bool _done { get; private set; }
        private string _particleCode = "";
        private long _spawned;
        public bool CanDispose { get; private set; }
        private bool _targetable;
        private byte _ran;
        private ParticleDefinition _particle;

        public ParticleHostEntityLogic(Entity entity) {
            _entity = entity;
            _blob = entity.Blob.FetchBlob("logic");
            _entity.Physics.PriorityChunkRadius(1, false);
            _done = false;
            CanDispose = false;
            _targetable = false;
            _ran = 0;
            _spawned = DateTime.Now.Ticks;
        }

        public override void PreUpdate(Timestep timestep, EntityUniverseFacade entityUniverseFacade) { }

        public override void Update(Timestep timestep, EntityUniverseFacade entityUniverseFacade) {
            if (!_done) {
                if (!string.IsNullOrWhiteSpace(_particleCode)) {
                    if (DateTime.Now.Ticks >= _spawned) {
                        BaseEffects.EmitParticles(_entity, _location, _particle.Code);
                        _spawned = DateTime.Now.AddSeconds(_particle.ParticleData.GetDouble("emitDuration")).Ticks;
                    }
                }
            }
        }

        public override void PostUpdate(Timestep timestep, EntityUniverseFacade entityUniverseFacade) {
            if (_done) {
                entityUniverseFacade.RemoveEntity(_entity.Id);
                CanDispose = true;
                Dispose();
            }
        }
        public override void Store() { }
        public override void Restore() { }
        public override void Construct(Blob arguments, EntityUniverseFacade entityUniverseFacade) { }
        public override void Bind() { }
        public override bool Interactable() {
            return false;
        }

        public override void Interact(Entity entity, EntityUniverseFacade facade, ControlState main, ControlState alt) { }
        public override bool CanChangeActiveItem() {
            return true;
        }

        public override Heading Heading() {
            return new Heading();
        }

        public override bool IsPersistent() {
            return false;
        }

        public override bool IsAtLastSavedPosition() {
            return false;
        }

        public override ChunkKey GetLastSavedPosition() {
            return new ChunkKey();
        }

        public override void StorePersistenceData(Blob data) { }
        public override void RestoreFromPersistedData(Blob data, EntityUniverseFacade facade) { }
        public override bool IsCollidable() {
            return false;
        }

        public new void Dispose() {
            base.Dispose();
            CanDispose = true;
        }

        public void SetParticleCode(string code) {
            _particleCode = code;
            _particle = GameContext.ParticleDatabase.GetParticle(_particleCode);
        }

        public void SetTargetable() {
            _targetable = true;
        }

        public void SetLocation(Vector3D location) {
            _location = location;
        }

        public void Finish() {
            _done = true;
        }
    }
}
