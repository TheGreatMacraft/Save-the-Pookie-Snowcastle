*Save the Pookie Snowcastle* is coop 2D pixel-art game about building and expending the snow castle whilst evil *christmas-themed* enemies attack the castle in waves. The concept is similar to [ResevanjePragozda](https://github.com/Gulcar/ResevanjePragozda/releases).

Snow is a magical resource that can build 
# Core Gameplay (Loop)

```bpmn
url: [[CoreGameplayLoop.bpmn]]
height: 300
opendiagram: false
```

The game is structured into waves. Each wave consists of random amount of different enemies, that provide a fair challenge for the players at all stages of progression.

In between stages, players have a certain amount of time to purchase new defense artillery, weapons, pets, etc.

# Game Mechanics
## Wave Handling
The game is structured into:
- *grace periods*, which allow the player to *strategically* build defenses, purchase pets, ... $\implies$ **prepare for the next wave**
- *wave periods*, which last from the game spawning the first enemy, to when the players kill the last enemy.
Each wave is divided into subwaves, which allows for the *progressively harder* feel of each individual wave.

```bpmn
url: [[WaveHandling.bpmn]]
height: 500
```

In order for this to work as intended, there will be 3 classes needed:
- **Wave** - a collection of *subwaves* and the amount of *grace time* players will receive after finishing the wave.
- **Subwave** - a collection of *enemy groups*.
- **Enemy Group** - it contains:
	- a *prefab* of the enemy that is supposed to be spawned,
	- total amount of enemies that are planned to be spawned,
	- an amount of enemies that have already spawned,
	- 2 values, in between which, a random one will be picked as "cooldown" between spawning of 2 enemies,
	- a *boolean* that will tell if the entire group has finished spawning

```bpmn
url: [[WaveClasses.bpmn]]
width: 500
```

## Grace Period
During the grace period players are to examine the damage, rebuild what they can, expand their defenses and also do something fun?

### Building
Players will have the ability to build during the grace period. They will unlock new defenses as they progress. Those will be visible in the building UI.

```bpmn
url: [[Building.bpmn]]
height: 475
```

# Enemies
### Bad snowman
This creature will throw snowballs at the castle.
### Gnomes
They will attack the players and attempt to disable defenses/traps. They will not attack the main castle until there is something else they can do. They will be shorter and will move at an average speed. Their smaller speed will make it **difficult** for defenses to hit them. Multiple gnomes disabling the same defense will manage to do that faster.

Their priority list: disarming defenses > hurting players > damaging the castle

```bpmn
url: [[GnomeBehaviourPattern.bpmn]]
```


1. **Snow golem** - Massive creature, that moves slowly, but deals a lot of damage when it gets a hit on it.
2. **Gingerbread man** - Fast and deals minimal damage to the castle. If it senses players nearby, it'll throw itself face first into snow, to camouflage.
### Boss Enemies
1. **Papa Gnome** - Bigger, slower enemy.
## Buildings
### Defenses
1. **Snow cannon** - can be mounted by a player and used to shoot snowballs. When upgraded, it fires automatically at closest enemy, it can reach.
### Assistance
1. **Radar** - Provides a mini-map until disabled or destroyed
## Weapons
1. **Hand** - Players can always throw snowballs with hands.
2. **Shotgun**
3. **Rifle**
4. **Shovel** - If players run out of snowballs mid fight, they can shovel the ground to get some.
## Pets
1. **White Fox** - moves quickly towards the toughest enemy and alters the players of its presence
2. **Polar Bear** - moves slowly towards the closes enemy it can catch and attacks them
3. **Black Kitty** - scares the monsters away, prolonging their destruction
# Artstyle
Winter-Christmas pixel art theme, inspired by Snowdin from Undertale and old minecraft christmas maps.

| ![[Pasted image 20251122130009.png]] | ![[Pasted image 20251122130051.png]] |
| ------------------------------------ | ------------------------------------ |

