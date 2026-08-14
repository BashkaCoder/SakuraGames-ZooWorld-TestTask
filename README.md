# Zoo World

## Overview

Zoo World is a top-down 3D Unity scene where frogs (prey) hop and snakes (predators) move continuously. Animals spawn every 1-2 seconds, collide through Unity physics, return from the configured visible gameplay area, resolve their food-chain encounters, and update uGUI counters for dead prey and predators.

## Unity version

Unity `6000.3.11f1`.

## How to run

1. Open the project with the recorded Unity version.
2. Open `Assets/_Project/Scenes/ZooWorld.unity`.
3. Press Play.

## Architecture

`AnimalDefinition` is immutable species data: role, typed prefab, locomotion definition, and spawn weight. Its ScriptableObject assets never hold runtime state.

`Animal` is a small Unity adapter for its collider. It delegates contacts to `AnimalEncounterResolver`, owns the authoritative `AnimalLifetime`, and delegates movement to `AnimalBody`. `AnimalLifetime.TryDie` is the one idempotent death transition; it records statistics exactly once before the GameObject is destroyed.

`LocomotionDefinition` creates an `IAnimalLocomotion` strategy. `HopLocomotion` is used by Frog and `LinearLocomotion` by Snake. `GameplayAreaBounds` supplies the explicitly authored playable rectangle; `ScreenReturnSteering` is pure boundary logic shared by all movement strategies.

`ZooLifetimeScope` is the Composition Root. It registers the small set of real variation/test boundaries, scene components, `AnimalSpawner`, domain interaction rules, and the VContainer-owned async spawn entry point. Dynamic typed prefabs are created through VContainer so injected components receive their dependencies. Spawn cadence is authored in `AnimalSpawnSettings`.

`AnimalDeathStatistics` publishes a simple change event; `DeathCounterPresenter` is its focused uGUI bridge. `TastyFeedback` only presents a successful consumption event next to the surviving predator.

## Patterns used

- Strategy: locomotion implementations are composed from `LocomotionDefinition` assets.
- Rules pipeline: `AnimalEncounterResolver` iterates `IAnimalContactRule` implementations.
- Observer/events: death statistics and consumption presentation.
- Composition Root + DI: VContainer wiring is isolated in `ZooLifetimeScope`.

## Scaling to 1000 animals

Adding Rabbit with prey + Hop locomotion needs a new prefab and `AnimalDefinition` only. Adding Bird with flight needs a `LocomotionDefinition`/`IAnimalLocomotion` pair, without changing `Animal` or `AnimalBody`. A future special encounter is an additional `IAnimalContactRule`; the resolver does not grow an interaction matrix.

## Testing

EditMode tests cover prey/prey non-resolution, predator/prey consumption, deterministic predator duels, duplicate-contact idempotency, separate death counters, and all four screen-return directions. A small PlayMode composition smoke test verifies that the configured scene spawns an injected animal.

## Intentional trade-offs

Runtime dependencies are VContainer, UniTask, URP and uGUI. There is no ECS (explicitly prohibited), Addressables, reactive framework, Input System, or pooling. Current spawn/destruction frequency does not justify their complexity. Primitive visuals are deliberately used because art is outside the task's evaluation target.
