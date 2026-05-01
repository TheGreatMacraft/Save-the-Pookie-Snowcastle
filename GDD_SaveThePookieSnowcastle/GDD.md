*Save the Pookie Snowcastle* is coop 2D pixel-art game about building and expending the snow castle whilst evil *winter-themed* enemies attack the castle in waves. The concept is similar to [ResevanjePragozda](https://github.com/Gulcar/ResevanjePragozda/releases).

| ![[Pasted image 20251207183120.png\|300]] | ![[Pasted image 20251207183351.png\|300]] |
| ----------------------------------------- | ----------------------------------------- |

# Table of Contents
```table-of-contents
```

# Gameplay
Gameplay must be [[Orthogonal|orthoganal]] - as players complete the waves, they will have to utilize new unlocks and *adapt different playstyles* to win.
An example of such approach can be seen in a horror co-op game [[Orthogonal#R.E.P.O.|R.E.P.O]]. 

## Basic Gameplay Overview
Players will begin the game by picking a [[#Classes|Class]]. Together they will *protect the village/settlement* by fighting off the enemies *themselves*, while simultaneously *using/managing buildings* to help them out.
Buildings won't just target enemies, but also provide upgrades of different sorts to the players.
After each wave, players will receive some downtime, which will let them expand their base and achieve new unlocks.

Variety will come from different classes, buildings, weapons, upgrades, etc. 
The goal is to not only provide many unique combat styles (tank, flanker, wizard, etc.), but also offer the possibility to lean more into "mange towers" play-style.

### Player
Depending on the selected *class* and the state of the *upgrades tree*, the players will have access to different weapons, tools and abilities, but they will be part of the same system.

1. **Loadout** - loadout will consist of a primary weapon and a secondary weapon - player will only be able to have 1 equipped at once
2. **Tools** - player will be able to chose a tool, that will grant some passive boost/effect
3. **Abilities** - player will be able to perform number of abilities (rolling), depending on the class

## Genre Breakdown
Main two genres present are [[#Action Combat]] and [[#Tower Defense]].

Players will strategize:
- during combat on where and how to push back the attackers
- during downtime on what upgrades to get and how to prepare

## Classes
Classes give the player ability to chose a class that aligns with *their desired playstyle* and is equal in *importance and value* with the rest.
At the same time, there must me *many different combinations of classes*, that together *fill all the required roles* - offence & defense.
*What playstyles  will my players want?* - [[#Selection]]

Classes must provide *variety* in playstyles, but *not lock* the player out of experiencing parts of other styles. It's important to provide *variety within a certain class* - [[#Cross-Class Evolution]]

### Selection
Each class needs to *support a broader playstyle* (specific archetypes will come with [[#Cross-Class Evolution]])

1. **Aggressor**
	What do the players want?
	- Constant action  - ADHD like (*high tempo of play*)
	- Progress onward
	- Feeling *powerful through intensity*
	How do they play?
	- Always moving and engaging
2. **Strategist**
	What do the players want?
	- Outsmarting enemies
	- Planning ahead
	- Controlling space
	How do they play?
	- Zone control

#### Flanker
Goal:
- **speed** - flanker must always **feel** like the *fastest* thing in their game view and in *control* of the *engagement distance*
- **positioning** - attacks from sides, back or blind spots - where he is *unexpected*
- **disruption** - breaks enemy formation, picks off isolated targets
- **quick escape** - *disappears quickly* before he can be punished

Mentality: 
"I *hit first*, I hit *where you don’t expect*, and *I leave* before you can punish me."

| Buffs             | Debuffs          |
| ----------------- | ---------------- |
| faster base speed | below average HP |
| more stamina      |                  |

Loadout:
- 

#### Area Denier
Goal:
- **control** - define *where* the opponents can be
- **hold ground** - they are *strongest in their area*

Mentality: 
"I decide where the fight happens."


| Buffs | Debuffs |
| ----- | ------- |
|       |         |

Tools:
- movable cannon - *variety* in bullet effects

#### Cross-Class Evolution
Whenever the player gains an upgrade, they may choose to **progress toward any class**. The upgrade they receive is a hybrid ability that combines their current class with the class they selected.

### Tank

## Action Combat


## Buildings


## Settlement
How 

---
## Gameplay Loops

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
## Attacking the Player
### Gingerbread man
Fast, dealing minimal damage. It's AI is very simple. It just runs towards the nearest player, to attack it.
### Bad snowman
This creature will throw snowballs at the castle.
## Attacking the Buildings
### Gnome
They will attack the players and attempt to disable defenses/traps. They will not attack the main castle until there is something else they can do. They will be shorter and will move at an average speed. Their smaller speed will make it **difficult** for defenses to hit them. Multiple gnomes disabling the same defense will manage to do that faster.

Their priority list: disarming defenses > hurting players > damaging the castle

```bpmn
url: [[GnomeBehaviourPattern.bpmn]]
```

## Attacking the Castle

1. **Snow golem** - Massive creature, that moves slowly towards the castle, but deals a lot of damage when it gets a hit on it.
### Boss Enemies
1. **Papa Gnome** - Bigger, slower enemy.
## Buildings
### Defenses
1. **Snow cannon** - automatically shoots enemies. It can also be mounted by the player, making it fire faster and counting towards any kill-specific quota. *slower shoot speed, stronger hits*
Upgrade ideas:
- ability to shoot at multiple targets at once
- faster shooting speed
- custom ammo

2. **
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

## Villagers
Unlocked through completion of levels. Their "stats" (speed, strength, ...) as well as traits (scared/ferocious, ...) and preferences (combat, books, ...) will vary and will be an important factor when the players will be managing them.

# Entity Behavior
All entities (enemies, pets, villagers, etc.) will share the same **base** logic. Of course swift support enemy, like gnome will have a different move set than a slow heavy hitter, like golem, but their classes will be derived from the same base.

Broken down into bare bones, every entity will:
- **move** towards certain point with a certain goal in mind
- upon *completion* of said goal or an *interruption*, the **goal will change** and the entity will begin moving again.

# Artstyle
Winter-Christmas pixel art theme, inspired by Snowdin from Undertale and old minecraft christmas maps.

| ![[Pasted image 20251122130009.png]] | ![[Pasted image 20251122130051.png]] |
| ------------------------------------ | ------------------------------------ |

![[pixel art snow.jpg]]
![[top down winter forest.png]]
![[winter side scroller.png]]

