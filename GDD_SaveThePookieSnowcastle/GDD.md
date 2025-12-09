*Save the Pookie Snowcastle* is coop 2D pixel-art game about building and expending the snow castle whilst evil *winter-themed* enemies attack the castle in waves. The concept is similar to [ResevanjePragozda](https://github.com/Gulcar/ResevanjePragozda/releases).


| ![[Pasted image 20251207183120.png\|300]] | ![[Pasted image 20251207183351.png\|300]] |
| ----------------------------------------- | ----------------------------------------- |

# Gameplay
The goal is to make the gameplay as orthogonal as possible. As players progress through the levels, which will seem the same, they will have to adapt new strategies and overall play differently.

An example of such approach can be seen in a horror co-op game *R.E.P.O*. Players in earlier stages don't have an effective way to deal with monsters, besides hiding and checking corners. However, as they earn money and purchase weapons, they now have an actual way to fight the monsters. **The game stays the same, but player's abilities and capabilities change and in term changing the gameplay completely.**

The goal is to progressively keep the game *interesting*, by adding variety and *familiar* by keeping the **core gameplay loop** roughly the same.

## Gameplay Loops

```bpmn
url: [[CoreGameplayLoop.bpmn]]
height: 300
opendiagram: false
```

All players will be doing in the game will be fighting monsters and reinforcing their defenses to fight off more monsters. The primary goal will always be the same: *Don't let the monsters destroy the snowcastle*.

However, as they progress, the ways of doing that will change. At first players won't have much, but some guns, snowballs, a cannon and a snowcastle. But with time, they will unlock more cannons, traps, guns, swords,... each with some quality of life upgrades along the way.

This will transform an originally very simple gameplay loop into a complex list of managing defenses, weapons, troops?, etc.
# Game Mechanics

## Player
### Camera Movement During Combat
When player is fighting the wave, camera is following their mouse cursor, to make it easier to see the enemies in the direction of attack.

```bpmn
url: [[CameraMoveToCursor.bpmn]]
height: 400
opendiagram: false
```

### Attacking
Whenever a player attacks an enemy, the camera will slightly shake in a way that won't disrupt the player or cause motion sickness.

## Weapons / Tools

### Ranged
1. **Icicle Launcher** - AK like weapon, that shoots multiple icicles in short succession. ==middle of the pack, multiple projectile gun==
2. **Snow Cannon** - It will fire a single, thicker snowball that will deal more damage and be more expensive to fire. It will move slower than the average projectile. Pressing the **special** button, will make the snowball explode into little snowballs, each flying into it's own direction. ==ranged, crowd control weapon==
3. **The Froster** - a powerful laser, that has a very long charge time. During the charge, the player can't move or cancel the attack. Hitting an enemy is meant to be very difficult, but once it is done, it deals a lot of damage. Similarly to *Sparky from Clash Royale*. ==High risk, high reward gun==
4. **Ego Tripper** - a shotgun-like weapon that doubles it's next shot damage, as long as the player kills an enemy within a displayed time frame. Having this weapon equipped, limits the player's max HP to 1. ==Tryhard Gun==
5. **Magic Wand** - primary that allows the player to cast spells, such as:
	- freeze - slows down the enemies within range
	- cold snap - makes snowballs fall out of the sky for a limited time within range
	- blizzard  - creates low visibility at the range, causing enemies to be disoriented and unable to attack
	==Wizard Wand==
### Melee
1. **Ice Sword** - a melee weapon ==middle of the pack melee weapon==
2. **Ice Axe** - a big axe, that has long wind attack time, but the attack can be released by releasing the wind button, at any time. While the attack is ready, player moves slower.
3. **Icicles** - daggers that can be used as a melee weapon or thrown ==mixed weapon==
4. 
### Tools
1. **Soul Catcher** - A secondary, that consumes the souls of victims, storing them into a primary weapon power up, that can be used later.
2. **Skis** - Increases the player movement speed and adds a sort of "glidy" feel
### Systems
### Gun Base Functionality
All guns will share a script, that will define their basic functionality. In addition, each gun will have it's own script, that will define it's niche functionality, but this script will be a starting point for all of them.

```bpmn
url: [[GunBaseFunctionality.bpmn]]
```
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
All enemies need to challenge the players in unique ways. Some will try to distract the player by attacking them and wasting their time, while others deal damage to defenses and the castle. **Player needs to deal with different enemies differently.**
## Enemies Attacking the Player
### Gingerbread man
Fast and deals minimal damage to the castle. If it senses players nearby, it'll throw itself face first into snow, to camouflage. If uncovered, it will try to run away and retreat to the castle in a little bit.
### Bad snowman
This creature will throw snowballs at the castle.
### Gnomes
They will attack the players and attempt to disable defenses/traps. They will not attack the main castle until there is something else they can do. They will be shorter and will move at an average speed. Their smaller speed will make it **difficult** for defenses to hit them. Multiple gnomes disabling the same defense will manage to do that faster.

Their priority list: disarming defenses > hurting players > damaging the castle

```bpmn
url: [[GnomeBehaviourPattern.bpmn]]
```


1. **Snow golem** - Massive creature, that moves slowly towards the castle, but deals a lot of damage when it gets a hit on it.
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

![[pixel art snow.jpg]]
![[top down winter forest.png]]
![[winter side scroller.png]]

