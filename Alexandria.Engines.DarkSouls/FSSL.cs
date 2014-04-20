using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glare.Internal;
using System.Resources;
using System.IO;
using Glare.Framework;
using Glare.Assets;

namespace Alexandria.Engines.DarkSouls {
	public class Fssl : Asset {
		static readonly string[] Translations_ai101000 = new string[] {
			"[ Random number ] random number generation", // 0
			"[ Timer ] timer start", // 1
			"[ Timer ] restart timer", // 2
			"[ Timer ] timer has been stopped", // 3
			"] [ Debug log output", // 4
			"[ Debug ] ticker output", // 5
			"[ Debug ] stop", // 6
			"[ Debug ] popcorn create", // 7
			"] [ Debug log output + Int three", // 8
			"] [ Debug log output + Float three", // 9
			"Set the [ general-purpose ] NPC behavior variable", // 10
			"[ Attack ] melee", // 11
			"[ Attack ] defense", // 12
			"[ Attack ] non-combat action end", // 13
			"[ Attack ] attack cancel", // 14
			"[ Attack ] combo attack cancel", // 15
			"[ Attack ] equipped weapon switching", // 16
			"[ Attack ] random avoidance", // 17
			"[ Attack ] random avoidance _ angle specified", // 18
			"[ Attack ] damage lower logic reset setting", // 19
			"[ Attack ] damage reset information", // 20
			"Reset the [ attack ] attack failed", // 21
			"Reset the [ attack ] damage type number", // 22
			"[ Attack ] resident damage type setting", // 23
			"[ Attack ] blade Dash Cancel", // 24
			"Use the [ action ] OBJ", // ​​25
			"Away from the [ action ] OBJ", // ​​26
			"Use the [ action ] position adjustment OBJ", // ​​27
			"Use of OBJ [ action ] navigation mesh specified", // 28
			"[ Action ] stand jump", // 29
			"Cancel the [ action ] Initiated state", // 30
			"Clear the action start-up reset state flag", // 31
			"To end the [ action ] boss special action", // 32
			"[ Mirror ] Knight summoned lottery operation", // 33
			"Climb the ladder [ action ]", // 34
			"Go down the [ action ] ladder", // 35
			"Climb in a hurry the [ action ] ladder", // 36
			"Go down in a hurry the [ action ] ladder", // 37
			"Jump off [ action ] ladder", // 38
			"Slide down the [ action ] ladder", // 39
			"[ Action ] ladder upward attack", // 40
			"[ Action ] ladder downward attack", // 41
			"[ Action ] special effects triggered", // 42
			"[ Action ] erasing special effects", // 43
			"Starting the [ action ] Shirotobira pass check", // 44
			"[ Operation ] invincible on", // 45
			"[ Operation ] damage on invalid", // 46
			"[ Operation ] invincible , damage invalid off", // 47
			"[ Operation ] SFX setting", // 48
			"[ General-purpose ] share flag set", // 49
			"[ General-purpose ] share flag value set", // 50
			"[ General-purpose low-order switching state map", // 51
			"Protect [ stone ] Fugitive request", // 52
			"[ Operation ] death request", // 53
			"[ Move ] move prediction accuracy setting", // 54
			"Reset the [ move ] move failed", // 55
			"Disable [ move ] move 'the'", // 56
			"[ Move ] usually random move", // 57
			"[ Move ] usually move navigation mesh", // 58
			"You would normally go to the World correction position of the moving target ]", // 59
			"[ Move ] move random lock", // 60
			"[ Move ] move navigation lock mesh", // 61
			"To lock move to position correction of the moving target ]", // 62
			"Move [ ] Dash _ random", // 63
			"Move [ ] _ dash navigation mesh", // 64
			"Dash to move the world correction position of the moving target ]", // 65
			"[ Move ] dash _ lock _ random", // 66
			"Move [ ] _ lock _ dash navigation mesh", // 67
			"To lock dash move to correct position of the moving target ]", // 68
			"[ Move ] horizontal movement _ random", // 69
			"[ Move ] horizontal movement _ navigation mesh", // 70
			"It moves horizontally to the world correction position of the moving target ]", // 71
			"To move horizontally at the center of the [ movement ] navigation mesh", // 72
			"To move horizontally at the center position of the navigation mesh the next move of the navigation route ]", // 73
			"[ Move ] horizontal movement _ lock _ random", // 74
			"[ Move ] horizontal movement _ lock _ navigation mesh", // 75
			"[ Move ] horizontal movement _ lock _ _ random direction specified", // 76
			"To lock horizontal movement to correct position of the moving target ]", // 77
			"To lock horizontal movement to the world correction position of the moving target ]", // 78
			"Move [ ] rise", // 79
			"[ Move ] down", // 80
			"[ Move ] Warp", // 81
			"Move [ ] _ warp generation generator", // 82
			"[ Move ] warp _ target", // 83
			"[ ] General-purpose logic group flag setting", // 84
			"And follow the offset position from move group leader ]", // 85
			"Offset position setting of group members move ]", // 86
			"Become general-purpose [ group leader ]", // 87
			"To set the [ ] platoon platoon flag", // 88
			"Setting the [ platoon ] weight magnification", // 89
			"[ Search ] navigation mesh", // 90
			"Navigation mesh to [ search ] generator", // 91
			"Navigation mesh to [ search ] navigation point", // 92
			"Navigation mesh to [ search ] Gen navigation point", // 93
			"Navigation mesh to [ search ] OBJ", // ​​94
			"Navigation mesh to the sound [ search ] heard", // 95
			"To offset position from the search group leader ] Navi mesh", // 96
			"Navigation mesh to the gate of the [ search ] near", // 97
			"[ Search ] navigation mesh to the point that [ debug ] click", // 98
			"[ Search ] navigation mesh to [ debug ] navigation graph test route starting point", // 99
			"[ Search ] navigation mesh to [ debug ] navigation graph test root end point", // 100
			"Overwrite current position in the [ search ] generator position", // 101
			"Root search settings [ search ] navigation mesh", // 102
			"Effective size setting of search Short navigation mesh", // 103
			"[ Turning ] quick turn _ orientation specified", // 104
			"[ Turning ] quick turn lock _", // 105
			"[ Turning ] quick turn _ Navi mesh direction", // 106
			"[ Turning ] quick turn _ sound direction", // 107
			"[ Turning ] quick turn _ generator direction", // 108
			"[ Turning ] on the spot turning lock _", // 109
			"[ Turning ] on the spot turning _ Navi mesh direction", // 110
			"[ ] Searching for the enemy character _ targeting", // 111
			"Searching for the enemy [ ] player _ targeting", // 112
			"[ ] Object search operation _ targeting", // 113
			"Searching for the enemy [ ] generation generator _ targeting", // 114
			"[ ] Searching for the enemy moving region _ targeting", // 115
			"Searching for the enemy [ ] _ attacker targeting", // 116
			"Searching for the enemy [ ] navigation point _ targeting", // 117
			"[ Gen ] searching for the enemy moving region _ targeting", // 118
			"Searching for the enemy [ ] Gen navigation point _ targeting", // 119
			"Attacker _ targeting to searching for the enemy [ ] group", // 120
			"Searching for the enemy [ ] _ Shirotobira targeting", // 121
			"To clear the search operation target ]", // 122
			"To save the [ target ] search operation", // 123
			"To load the [ target ] search operation", // 124
			"To the players who are updating their search operation [ ] target", // 125
			"[ ] Search operation last sighting information overwrite", // 126
			"[ ] Search operation last sighting information erasing", // 127
			"[ Operation ] lock-on", // 128
			"[ Operation ] lock-off", // 129
			"[ Operation ] lock list described", // 130
			"[ Player ] attack attack", // 131
			"Attack [ player ] switching equipment", // 132
			"[ Player ] attack spell use", // 133
			"Change how you hold [ player ] weapon attack", // 134
			"Attack [ player ] item use", // 135
			"Attack [ player ] gesture use", // 136
			"Acquisition of random numbers", // 137
			"Acquisition of timer value", // 138
			"Acquisition of state elapsed time", // 139
			"Acquisition of random values ​​of the state elapsed time for", // 140
			"Acquisition of common parameters", // 141
			"Events get flag", // 142
			"Action loop time around magnification get", // 143
			"Or ready for attack", // 144
			"Or attacking", // 145
			"Or non-combat action in", // 146
			"Or avoidance of", // 147
			"Or defending", // 148
			"Or damage during", // 149
			"Are you shaking the attack", // 150
			"'The' or thrown in", // 151
			"Current combo number obtaining", // 152
			"Count acquisition damage type", // 153
			"Get attack failed", // 154
			"Or waiting", // 155
			"Or blade Dash", // 156
			"Do you faster than walking speed", // 157
			"Or spell being prohibited", // 158
			"Or swoon in", // 159
			"'The' should be pursued", // 160
			"Follow-up action number acquisition", // 161
			"Follow-up action distance acquisition", // 162
			"'The' should be behind attack", // 163
			"Behind aggressive behavior number acquisition", // 164
			"Behind aggressive behavior distance acquisition", // 165
			"Can you use a recovery item", // 166
			"Avoidance acquisition type", // 167
			"Compared to the target state", // 168
			"Compared to the state of their own", // 169
			"Distance between the position correction", // 170
			"Target injection Barrett check", // 171
			"Correction movement Confirmation", // 172
			"Or OBJ operation", // 173
			"Or OBJ in use", // 174
			"OBJ someone use", // 175
			"Did you fail to use the OBJ", // ​​176
			"OBJ get type", // 177
			"OBJ is corrupt", // 178
			"Or jumping", // 179
			"Start-up or state:", // 180
			"'The' or has been activated state reset", // 181
			"Boss or special behaving", // 182
			"Boss or special action terminating", // 183
			"Mirrors Night spirits preparation has completed", // 184
			"Do you recall allow a mirror Night NPC", // ​​185
			"Start-up state ID acquisition", // 186
			"Or special effects triggered during", // 187
			"Are you sure that is reflected in camera", // 188
			"'The' complex or visible", // 189
			"Get priority action", // 190
			"Action is executable", // 191
			"And whether the contact with the target", // 192
			"Is it possible to throw a target", // 193
			"Acquisition of weapon equipment", // 194
			"Acquisition of weapons destruction", // 195
			"Target weapons or long-range system", // 196
			"Acquisition of equipment or weapon available", // 197
			"HP acquisition", // 198
			"HP acquisition absolute value", // 199
			"HP site acquisition", // 200
			"The amount of damage", // 201
			"The amount of damage OBJ", // ​​202
			"The amount of damage MAP", // ​​203
			"Site the amount of damage", // 204
			"Reset from the damage was executed or", // 205
			"'The' player or opponent camp that has been attacking", // 206
			"Or guard success", // 207
			"'The' or grabbed", // 208
			"And you are in a bright place", // 209
			"Site is corrupt", // 210
			"Logic level acquisition", // 211
			"Get the state of the route search", // 212
			"Route search move or have completed", // 213
			"Route search was successful", // 214
			"Distance from the end point of the path", // 215
			"Distance of a route runner-up", // 216
			"Vertical distance of a route runner-up", // 217
			"Angle of a route runner-up", // 218
			"Point that there is a goal and end point of the path is the same or", // 219
			"Get click point update", // 220
			"Get moving failed", // 221
			"'The' or in contact with the ground", // 222
			"Or quick turn in", // 223
			"Movement prediction accuracy acquisition", // 224
			"Or forceful feedback area", // 225
			"Whether there is a need to use the object for movement", // 226
			"Do you need to climb the ladder for the mobile", // 227
			"One step on someone dolphin", // 228
			"One step under someone dolphin", // 229
			"Do you need to get off the ladder for the mobile", // 230
			"Do you need to get off the wall for the mobile", // 231
			"Are you riding in the navigation mesh", // 232
			"Distance with the navigation mesh center", // 233
			"Distance with the navigation mesh center of the next", // 234
			"'The' should be warp", // 235
			"Feedback extended range", // 236
			"Return method", // 237
			"Feedback during setback distance", // 238
			"Retreat time feedback during", // 239
			"Do you examine the sound feedback when", // 240
			"Logic group flag get", // 241
			"Distance between the group offset position", // 242
			"Group member number", // 243
			"Group number", // 244
			"Vertical distance between the group offset position", // 245
			"Or there are characters that we have to attack the group", // 246
			"Shared flag set number of people get", // 247
			"Distance between the group members", // 248
			"Angle between the group members", // 249
			"Range angle between the group members", // 250
			"Distance between all group members", // 251
			"Angle with all group members", // 252
			"Range angle between all group members", // 253
			"Group members or was attacked from the player", // 254
			"Or onlookers in", // 255
			"Onlookers level acquisition", // 256
			"Platoon flag get", // 257
			"Platoon flag is get the number of members of the specified value", // 258
			"Platoon and the target is the same or", // 259
			"Flag share acquisition", // 260
			"Sub- state ID acquisition", // 261
			"NPC action ID acquisition", // 262
			"NPC behavior variable acquisition", // 263
			"NPC character local variable get", // 264
			"NPC conversation finish flag get", // 265
			"NPC conversation get", // 266
			"Events get variable", // 267
			"Gen event flag get", // 268
			"Gen event flag setting check", // 269
			"Get possession item number", // 270
			"Player or bonfire in use", // 271
			"Gen the waiting time acquisition", // 272
			"Or boss races", // 273
			"Or ready for attack player", // 274
			"Player acquisition equipment slot", // 275
			"Or a player can activate spell equipment", // 276
			"Player or a spell usable state", // 277
			"Check how to hold the weapon of the player", // 278
			"The item in use", // 279
			"Or gesture in use", // 280
			"Are you equipped the item", // 281
			"Weapon category of player acquisition", // 282
			"Distance from the target", // 283
			"Horizontal distance from the target", // 284
			"Vertical distance from the target", // 285
			"Angle with respect to the target", // 286
			"Angular range check and the target", // 287
			"Vertical and horizontal angle range check and the target", // 288
			"Angle from the target", // 289
			"Angular range check from the target", // 290
			"Horizontal distance between the target offset", // 291
			"Horizontal angle of offset between the target", // 292
			"Check distance between the target future position", // 293
			"HP acquisition of goal", // 294
			"Distance of a target Damipori", // 295
			"Angle of Damipori goals and", // 296
			"The goal is you are in a bright place", // 297
			"Distance between the generator that is generated", // 298
			"Gen distance between the generator that is generated", // 299
			"Gen distance % of the generator that is generated", // 300
			"Distance check of players", // 301
			"Distance of Damipori players", // 302
			"Angle of Damipori players", // 303
			"The goal is you are in tracking out of range", // 304
			"The goal or no-entry area", // 305
			"The goal or Gen no entry area", // 306
			"Distance between the moving region", // 307
			"Do not lock area", // 308
			"Distance of the movement area and the player", // 309
			"Distance with the navigation point", // 310
			"Distance between the Gen navigation point", // 311
			"Distance of the movement area and target", // 312
			"If there is an obstacle to the target", // 313
			"Whether there is an obstacle that inhibits the move", // 314
			"Front or in contact with the wall", // 315
			"If there is a wall in front", // 316
			"If there is an object in front", // 317
			"If there is a wall in front 2", // 318
			"If there is an object in front 2", // 319
			"Fault determination", // 320
			"'The' or not pressed", // 321
			"Distance between the Gen movement area", // 322
			"Distance between the Gen feedback region", // 323
			"Gen distance of movement area and the player", // 324
			"Whether there is a Gen movement area", // 325
			"Distance of Gen move the target area", // 326
			"Whether the torch in", // 327
			"Or torches range", // 328
			"Get number of players", // 329
			"Target line-of-sight or interrupted a certain period of time", // 330
			"To investigate whether the same ladder", // 331
			"To investigate whether one step on the ladder", // 332
			"To investigate or one step below the ladder", // 333
			"Goal is set", // 334
			"Target type", // 335
			"Whether lock-on during", // 336
			"Search operation has completed", // 337
			"'The' whether it is locked on", // 338
			"If there is a last sighting information", // 339
			"It is already described in the lock list", // 340
			"The goal or the field of view", // 341
			"Sound or heard", // 342
			"Generated sound horizontal angle acquisition", // 343
			"Should toward the Shirotobira", // 344
			"Did you participate in the boss fight", // 345
			"[ Timer ] timer reset", // 346
			"Get the flag", // 347
			"Sub- state : Fugitive ( Navi 1)", // 348
			"Start state", // 349
			"Sub- state : start", // 350
			"Sub- state : ready", // 351
			"Navigation point move _SubState", // ​​352
			"[ LIB ] initial completion set _SubState", // ​​353
			"Sub- state : combat", // 354
			"Sub- state : set", // 355
			"Sub- state : waiting branch", // 356
			"[ LIB ] subordinate action : branch _SubState", // ​​357
			"[ LIB ] subordinate action : action stop _SubState", // ​​358
			"[ LIB ] generator action (lower ) _SubState", // ​​359
			"Target behavior : bare hands _SubState", // ​​360
			"[ LIB ] OBJ destructive behavior _SubState", // ​​361
			"[ LIB ] : Lower sound reaction _SubState", // ​​362
			"[ LIB ] : Lower loss behavior (pursuit ) _SubState", // ​​363
			"[ LIB ] target behavior : feedback error _SubState", // ​​364
			"[ LIB ] Target : Yes non- visible ( random walk ) _SubState", // ​​365
			"[ LIB ] target without action ( random walk ) _SubState", // ​​366
			"First Contact : General-purpose round trip", // 367
			"[ LIB ] route round trip : the normal movement _SubState", // ​​368
			"First Contact : General-purpose branch round trip", // 369
			"[ LIB ] root branch round trip : the normal movement _SubState", // ​​370
			"First Contact : General-purpose loitering", // 371
			"[ LIB ] wandering route : normal movement _SubState", // ​​372
			"First Contact : General-purpose round trip ( slow)", // 373
			"[ LIB ] round-trip route : horizontal movement _SubState", // ​​374
			"First Contact : General-purpose branch round trip ( slow)", // 375
			"[ LIB ] root branch round trip : horizontal movement _SubState", // ​​376
			"First Contact : General-purpose wandering ( slow)", // 377
			"[ LIB ] wandering route : horizontal movement _SubState", // ​​378
			"First Contact : surprise attack : Generic: normal", // 379
			": Lower First Contact", // 380
			": Lower turning", // 381
			": Single-shot action : general purpose [ LIB ] attacks ( bare hands : scratch ) _SubState", // ​​382
			"First Contact : surprise attack : Generic: Blade Dash", // 383
			"First Contact : ( House of Ann Deal ) to escape to in a cage", // 384
			": Lower setting", // 385
			"Orinige : Search generator", // 386
			"Orinige : to escape to cage", // 387
			"Orinige : turning", // 388
			"Orinige : random wandering", // 389
			"Orinige : loitering wait", // 390
			"Orinige : encounter with the PC", // ​​391
			"Orinige : Wait", // 392
			"Orinige : getaway in the cage destruction", // 393
			"Navigation point movement ( far ) _SubState", // ​​394
			": ( Sinus gap ) rustling the grass first contact", // 395
			"Sub- state : start-up error", // 396
			"Rustling _SubState", // ​​397
			"Navigation point move", // 398
			"Nabipoin Jump to: start", // 399
			"Nabipoin Jump to: Search to the navigation 1", // 400
			"Nabipoin Jump to: navigation move one success", // 401
			"Nabipoin Jump to: wait for the arrival of destination", // 402
			"End -state", // 403
			"Target behavior : bare hands", // 404
			"Target action : start", // 405
			"Target action : action branch", // 406
			"[ LIB ] damage behavior : Com para priority _SubState", // ​​407
			"Aggressive behavior : bare hands _SubState", // ​​408
			"Reach action ( General-Purpose beast ) _SubState", // ​​409
			"[ LIB ] move ( four-legged general purpose ) _SubState", // ​​410
			"[ LIB ] remote attack corresponding _SubState", // ​​411
			"[ LIB ] Tomaki action _SubState", // ​​412
			"[ LIB ] follow-up action _SubState", // ​​413
			"Aggressive behavior : bare hands", // 414
			"Aggressive behavior : start", // 415
			"Aggressive behavior : the close-distance branch", // 416
			"Aggressive behavior : middle distance branch", // 417
			"[ LIB ] attack : General-purpose it ( Navi check) : dredge ll掻: _SubState near", // 418
			"[ LIB ] Attack: general purpose ( Navi check) : The dredge ll掻: _SubState medium", // 419
			"The [ LIB ] Attack: scratch : general-purpose _SubState near", // 420
			"The [ LIB ] Attack: scratch : general-purpose _SubState medium", // 421
			"[ LIB ] behind attack _SubState", // ​​422
			"Attack success end -state", // 423
			"Attack failed end -state", // 424
			"Minimum time", // 425
			"Maximum time", // 426
			"Reach distance", // 427
			"Avoidance probability", // 428
			"Avoidance type", // 429
			"Reach action ( General-Purpose beast )", // 430
			"Reach action : branch", // 431
			"Reach action : correction position move", // 432
			"Reach action : avoid", // 433
			"Reach action : failure processing", // 434
			"Reach action : right-handed move", // 435
			"Reach action : left turn movement", // 436
			"Start navigation point ID", // ​​437
			"Navi end point ID", // ​​438
			"Random ratio", // 439
			"Navigation point movement ( far )", // 440
			"Getaway : Start", // 441
			"Getaway : target ( away )", // 442
			"Getaway : Search", // 443
			"Getaway : Navi Dash", // 444
			"Getaway : Search Exit", // 445
			"Getaway : random move", // 446
			"Getaway : Target (random )", // 447
			"Rustling", // 448
			"Rustling : forward", // 449
			"Rustling : advancing wait", // 450
			"Rustling : turning", // 451
			"Rustling : Wait", // 452
			"Rustling : Start", // 453
			"Rustling : recession", // 454
			"Rustling : retreat after turning", // 455
			"OBJ destructive behavior", // 456
			"OBJ test distance", // 457
			"Return start movement type", // 458
			"Feedback during movement type", // 459
			"Sharp turn angle", // 460
			"'The' or defense", // 461
			"[ LIB ] feedback action (lower)", // 462
			"Return action : start", // 463
			"Action feedback : Feedback Start", // 464
			"Return action : Search generator", // 465
			"Return action : retreat action", // 466
			"Return action : Navigation mesh center movement", // 467
			"Return action : reverse start", // 468
			"Action feedback : Feedback can point search", // 469
			"Action feedback : Feedback not set", // 470
			"Return behavior : self- return start", // 471
			"Return behavior : self- return success", // 472
			"Return action : risk aversion", // 473
			"Return behavior : self- feedback recession", // 474
			"Return action : 1 point Navi search", // 475
			"Return action : navigation search point 2", // 476
			"Return action : emergency stop", // 477
			"Return action : to the generator turn direction", // 478
			"Return action : Wait", // 479
			"Return action : sound reaction", // 480
			"Return action : sound warning", // 481
			"Return action : enemy in sight", // 482
			"Return action : non- visible", // 483
			"[ LIB ] Jump to: general-purpose navigation Jump to: Dash _SubState", // ​​484
			"[ LIB ] Jump to: navigation universal movement : walking _SubState", // ​​485
			"Non- combat : combat type", // 486
			"Movement type", // 487
			"[ LIB ] Target : Yes non- visible ( random walk )", // 488
			"Non- visible ( Tage existence) : Start", // 489
			"Non- visible ( Tage existence) : Waiting", // 490
			"Non- visible ( Tage existence) : move", // 491
			"Non- visible ( Tage existence) : non- combat", // 492
			"Non- visible ( Tage existence) : non-combat action waiting", // 493
			"Non- visible ( Tage existence) : risk aversion", // 494
			"Non- visible ( Tage existence) : specified direction ( 45 degrees)", // 495
			"Non- visible ( Tage existence) : specified direction ( 90 degrees)", // 496
			"Non- visible ( Tage existence) : specified direction ( 135 degrees)", // 497
			"Non- visible ( Tage existence) : specified direction (180 degrees)", // 498
			"Non- visible ( Tage existence) : specified direction ( 225 degrees)", // 499
			"Non- visible ( Tage existence) : specified direction ( 270 degrees)", // 500
			"Non- visible ( Tage existence) : specified direction ( 315 degrees)", // 501
			"Non- visible ( Tage existence) : The orientation specified branch", // 502
			"[ LIB ] target without action ( random walk )", // 503
			"Target without action : start", // 504
			"Target without action : Wait", // 505
			"Target without action : Feedback", // 506
			"Target without action : move", // 507
			"Target without action : non- combat", // 508
			"Target without action : non-combat action waiting", // 509
			"Target without action : risk aversion", // 510
			"Target without action : orientation specified branch", // 511
			"Target without action : orientation specified ( 45 degrees)", // 512
			"Target without action : orientation specified ( 90 degrees)", // 513
			"Target without action : orientation specified ( 135 degrees)", // 514
			"Target without action : orientation specified (180 degrees)", // 515
			"Target without action : orientation specified ( 225 degrees)", // 516
			"Target without action : orientation specified ( 270 degrees)", // 517
			"Target without action : orientation specified ( 315 degrees)", // 518
			"[ LIB ] subordinate action : branch", // 519
			"Action : branch", // 520
			"Action: The error", // 521
			"End -state target _ Yes action", // 522
			"End -state target _ inaction", // 523
			"End state _ generator action", // 524
			"End state _ lower stop action", // 525
			"End state _ not visible action", // 526
			"End state _ vanishing act", // 527
			"End state _ sound reactive behavior", // 528
			"End state _ return preparation", // 529
			"[ LIB ] subordinate action : stop action", // 530
			"Sub- stop action : start", // 531
			"Lower stop action : stopped", // 532
			"[ LIB ] short-range behavior : turning left and right", // 533
			"Turning left and right : start", // 534
			"Turning left and right : branch", // 535
			"Turning left and right : the short distance turning left", // 536
			"Turning left and right : a short distance turn right", // 537
			"Turning left and right : long distance turning left", // 538
			"Turning left and right : long distance turn right", // 539
			"Turning left and right : Exit", // 540
			"[ LIB ] initial end setting", // 541
			"Initial end setting : start", // 542
			"[ LIB ] OBJ destructive behavior", // 543
			"OBJ destroyed: start", // 544
			"OBJ destroyed: test", // 545
			"OBJ destroyed: lock-off", // 546
			"OBJ destroyed: run", // 547
			"OBJ destroyed: End setting", // 548
			"Success : the end -state", // 549
			"Failed: End State", // 550
			"Attack type", // 551
			"Short navigation mesh check", // 552
			": Single-shot action : [ LIB ] general-purpose attack", // 553
			"Attack: attacking", // 554
			"Attack: waiting Cancel", // 555
			"Attack: Success: the end -state", // 556
			"Attack: Failure: End State", // 557
			"Defense ON switch", // 558
			"[ LIB ] damage behavior : Com para priority", // 559
			"Damage action : start", // 560
			"Damage behavior : avoidance priority pattern", // 561
			"Damage action : retreat , the swing and avoid the end", // 562
			"Damage behavior : avoidance ( step )", // 563
			"Damage action : Wait thrown", // 564
			"Damage action : attack priority pattern", // 565
			"Damage action : move priority pattern", // 566
			"Action Damage : Defense priority pattern", // 567
			"Damage action : Priority no pattern", // 568
			"Action Damage : Defense ON", // ​​569
			"Damage action : initial standby", // 570
			"Damage behavior : avoidance ( rolling )", // 571
			"[ LIB ] short-range behavior : turning left and right _SubState", // ​​572
			"[ LIB ] recession defense behavior : the probability specified _SubState", // ​​573
			"Action type", // 574
			"Attack distance", // 575
			"Attack angle", // 576
			"[ LIB ] attack : general-purpose", // 577
			"Attack : start", // 578
			"Attack : attack before turning", // 579
			"Attack : move before turning", // 580
			"Attack : the normal movement", // 581
			"Attack: Dash", // 582
			"Attack: Search", // 583
			"Attack: Navi move", // 584
			"Attack: Dash Navigation", // 585
			"Attack : run", // 586
			"Attack : attack failed", // 587
			"Attack time-out end -state", // 588
			"[ LIB ] : Lower reaction sound track", // 589
			"Sound reaction : Start", // 590
			"Sound reaction : Navi search", // 591
			"Sound reaction : enemy decision", // 592
			"Sound reaction : not found", // 593
			"Sound reaction : risk aversion", // 594
			"[ LIB ] Jump to: navigation general-purpose move _SubState", // ​​595
			"[ LIB ] OBJ operation", // 596
			"OBJ Operation: Start", // 597
			"OBJ Operation: ladder use start", // 598
			"OBJ Operation: climb ladder", // 599
			"OBJ Operation: Go down the ladder", // 600
			"OBJ Operation: door use", // 601
			"OBJ operation : use rope", // 602
			"OBJ operation : use failure", // 603
			"OBJ operation : top attack", // 604
			"OBJ operation : under attack", // 605
			"OBJ How To Play: Use Exit", // 606
			"OBJ operation : hand over", // 607
			"OBJ successful use state", // 608
			"OBJ use failed state", // 609
			"Fast-moving type" , // 610
			"Slow moving type" , // 611
			"Analog strength" , // 612
			"Automatic brake" , // 613
			"[ LIB ] : Lower loss action ( follow-up )" , // 614
			"Disappearance (LS): Start" , // 615
			"Disappearance (LS): Navi search" , // 616
			"Disappearance (LS): not found" , // 617
			"Disappearance (LS): risk aversion" , // 618
			"Reach" , // 619
			"Sharp turn occurs angle" , // 620
			"Dash switch" , // 621
			"Navi search" , // 622
			"[ LIB ] Jump to: navigation universal human type move" , // 623
			"Navi Jump to: start" , // 624
			"Navi Jump to: navigation search" , // 625
			"Navi Jump to: steep turn" , // 626
			"Navi move : Dash" , // 627
			"Nabis movement : walking" , // 628
			"Navi Jump to: risk aversion move [ random ]" , // 629
			"Navi Jump to: end" , // 630
			"Navi Jump to: periodic check" , // 631
			"Navi Jump to: branch" , // 632
			"Navi Jump to: center movement" , // 633
			"Navi Jump to: risk aversion [ obstacle ]" , // 634
			"Navi Jump to: unreachable" , // 635
			"Navigation Jump to: search root success" , // 636
			"Navigation Jump to: search route failure" , // 637
			"Nabis movement : the center of the next" , // 638
			"[ LIB ] OBJ operation _SubState", // ​​639
			"The [ LIB ] move : _SubState wall down" , // 640
			"Obstacle : the end -state" , // 641
			"Unreachable : the end -state" , // 642
			"Recession probability" , // 643
			"[ LIB ] recession defense behavior : the probability specified" , // 644
			"Defense recession : start" , // 645
			"Defense recession : retreat action" , // 646
			"Defense retreat : turning" , // 647
			"Defense recession : branch" , // 648
			"Middle distance ( walking )" , // 649
			"Short-range ( horizontal backward )" , // 650
			"Flag clear period" , // 651
			"Dash navigation mesh short distance" , // 652
			"Analog intensity minima" , // 653
			"Analog maximum strength" , // 654
			"Re- approach distance" , // 655
			"[ LIB ] move ( four-legged general purpose )" , // 656
			"Approach : start" , // 657
			"Approach : approach behavior" , // 658
			"Approach : Forced move move failure" , // 659
			"Reach adjustment" , // 660
			"Clear flag" , // 661
			"Flag set" , // 662
			"[ LIB ] Jump to: navigation general-purpose four-legged move _SubState", // ​​663
			"Search" , // 664
			"[ LIB ] Jump to: navigation general-purpose four-legged move" , // 665
			"Firing range" , // 666
			"Maximum range distance" , // 667
			"Attack time" , // 668
			"Attack firing angle" , // 669
			"Orientation adjustment type" , // 670
			"Combo break angle" , // 671
			"[ LIB ] Attack: general purpose ( Navi )" , // 672
			"Attack : search" , // 673
			"Attack : Re Navi move" , // 674
			"Attack: Re- dash navigation" , // 675
			"Short Navi check" , // 676
			"Timeout" , // 677
			"[ LIB ] Jump to: navigation general-purpose move" , // 678
			"Navi Jump to: arrival Exit" , // 679
			"Movement success : the end -state" , // 680
			"Slowdown : the end -state" , // 681
			"Search : the end -state" , // 682
			"[ LIB ] target behavior : feedback error" , // 683
			"Return preparation : move left and right" , // 684
			"Return preparation : navigation mesh search" , // 685
			"Return preparation : move branch" , // 686
			"Return preparation : rearward movement" , // 687
			"Return preparation : mesh center movement" , // 688
			"Guard propriety" , // 689
			"How to move" , // 690
			"Remote distance corresponding end" , // 691
			"Remote corresponding end time" , // 692
			"Short dash navigation mesh" , // 693
			"Do be blurred on the left and right" , // 694
			"[ LIB ] ranged attack support" , // 695
			"Ranged attacks correspondence : start" , // 696
			"Ranged attacks correspondence : Wait" , // 697
			"Ranged attacks correspondence : guard run" , // 698
			"Ranged attack support : target path exploration" , // 699
			"Ranged attacks correspondence : generator path exploration" , // 700
			"Ranged attacks correspondence : Short Navi check (5m or less)" , // 701
			"Ranged attacks correspondence : Rock movement ( dash )" , // 702
			"Ranged attacks correspondence : Navi move ( dash )" , // 703
			"Ranged attacks correspondence : Rock movement ( usually )" , // 704
			"Ranged attacks correspondence : Navi move ( usually )" , // 705
			"Ranged attacks correspondence : random move" , // 706
			"Ranged attacks correspondence : Success Exit" , // 707
			"Ranged attacks correspondence : Failure Exit" , // 708
			"Ranged attacks correspondence : Rock movement ( horizontal )" , // 709
			"Ranged attacks correspondence : navigation movement ( horizontal )" , // 710
			"Ranged attack support : The orientation adjustment" , // 711
			"Ranged attacks correspondence : quick turn" , // 712
			"Ranged attacks correspondence : quick turn wait" , // 713
			"Ranged attacks correspondence : Short Navi Check (10m or less)" , // 714
			"Ranged attacks correspondence : Short Navi Check (15m or less)" , // 715
			"Ranged attacks correspondence : Navi move risk aversion (stop)" , // 716
			"Ranged attacks correspondence : Navi move risk aversion ( point return )" , // 717
			"Ranged attacks correspondence : Navi risk aversion move (direction adjustment)" , // 718
			"Ranged attacks correspondence : Timeout Exit" , // 719
			"Ranged attacks correspondence : Short Navi check branch" , // 720
			"Ranged attacks correspondence : arrival determination" , // 721
			"Ranged attacks correspondence : generator feedback Navi movement ( horizontal )" , // 722
			"Ranged attack support : exploration path interpolation behavior ( generator)" , // 723
			"Ranged attack support : exploration path interpolation behavior (target)" , // 724
			"Ranged attacks correspondence : bypass" , // 725
			"Success end state" , // 726
			"Failure end state" , // 727
			"Time-out end -state" , // 728
			"Upper limit standby generation and execution time (ON time )" , // 729
			"Lower limit standby generation and execution time (ON time )" , // 730
			"Standby ON / OFF", // ​​731
			"Round ON / OFF", // ​​732
			"[ LIB ] route round trip : the normal movement" , // 733
			"FC: start" , // 734
			"FC: Search : to the navigation point 1 (up)" , // 735
			"FC: move : to generator" , // 736
			"FC: move : to the navigation point 1 (up)" , // 737
			"FC: move : to the navigation point 2" , // 738
			"FC: Standby : 1 move (up) Navi" , // 739
			"FC: Standby : 2 move Navi" , // 740
			"FC: waiting : Genetics move" , // 741
			"FC: Search : Navi point 2or generator judgment" , // 742
			"FC: Search : to generator" , // 743
			"FC: Search : to the navigation point 1 (down )" , // 744
			"FC: move : to the navigation point 1 (down )" , // 745
			"FC: Standby : 1 move (down ) navigation" , // 746
			"FC: Search : to the navigation point 2" , // 747
			"[ LIB ] root branch round trip : the normal movement" , // 748
			"FC: branch : navigation point 2or generator judgment" , // 749
			"[ LIB ] wandering route : normal movement" , // 750
			"FC: Search : to the navigation point 1" , // 751
			"FC: move : to the navigation point 1" , // 752
			"FC: Standby : 1 Navi move" , // 753
			"FC: branch : Navi 2or Genetics" , // 754
			"FC: branch : Navi 1or Genetics" , // 755
			"FC: branch : Navi Navi 1or 2" , // 756
			"Turn switch" , // 757
			"[ LIB ] round-trip route : slow-moving" , // 758
			"FC: turning : quick turn (Up Navi 1)" , // 759
			"FC: turning : quick turn ( to the navigation 2 )" , // 760
			"FC: turning : quick turn ( to generator )" , // 761
			"FC: turning : quick turn ( down Navigation 1)" , // 762
			"FC: turning : turning the place (Up Navigation 1)" , // 763
			"FC: turning : turning the place ( to the navigation 2 )" , // 764
			"FC: turning : turning the place ( to generator )" , // 765
			"FC: turning : turning the place ( down navigation )" , // 766
			"[ LIB ] root branch round trip : slow-moving" , // 767
			"[ LIB ] wandering route : slow-moving" , // 768
			"FC: turning : quick turn ( to the navigation 1)" , // 769
			"FC: turning : turning the place ( to the navigation 1)" , // 770
			"Time off the wall" , // 771
			"[ LIB ] Move: Get off the wall" , // 772
			"'The' off the wall : Adjust angle" , // 773
			"Off the wall : start move" , // 774
			"Off the wall : determination end" , // 775
			"Off the wall : start" , // 776
			"Off the wall : it approaches to the wall" , // 777
			"Distance Lv1", // ​​778
			"Distance Lv2", // ​​779
			"Distance Lv3", // ​​780
			"Distance Lv4", // ​​781
			"Attack probability" , // 782
			"[ LIB ] Tomaki action" , // 783
			"Tomaki action : default" , // 784
			"Tomaki action : branch" , // 785
			"Tomaki action : rearward movement" , // 786
			"Tomaki action : forward movement" , // 787
			"Tomaki action : end processing" , // 788
			"Tomaki action : start" , // 789
			"Tomaki action : center movement" , // 790
			"Tomaki action : attack" , // 791
			"[ LIB ] Jump to: navigation general-purpose humanoid move _SubState", // ​​792
			"[ LIB ] Damage action : terrain damage _SubState", // ​​793
			"[ LIB ] behind attack" , // 794
			"Attack : Exit" , // 795
			"Attack: Dash Cancel" , // 796
			"Follow-up action" , // 797
			"Pursuit distance" , // 798
			"[ LIB ] follow-up action" , // 799
			"Pursuit : Com para use determination" , // 800
			"Pursuit : Exit" , // 801
			"Pursuit : Start" , // 802
			"[ LIB ] Attack: Generic: Parameters used _SubState", // ​​803
			"[ LIB ] Attack: Generic: action specified _SubState", // ​​804
			"[ LIB ] Damage action : terrain Damage" , // 805
			"OBJ avoid damage : start" , // 806
			"OBJ damage preventing : root search" , // 807
			"OBJ damage avoidance : avoidance behavior" , // 808
			"OBJ damage preventing : step" , // 809
			"OBJ avoid damage : rolling" , // 810
			"OBJ avoid damage : Dash" , // 811
			"OBJ damage preventing : Exit" , // 812
		};

		public const string Magic = "FSSL";

		public ReadOnlyList<string> Strings { get; private set; }

		public ReadOnlyList<FsslThing2> Thing2s { get; private set; }

		public ReadOnlyList<FsslThing2> UnusedThing2s { get; private set; }

		public ReadOnlyList<FsslThing3> Thing3s { get; private set; }

		public ReadOnlyList<FsslWeird> Weirds { get; private set; }

		public ReadOnlyList<FsslThing4> Thing4s { get; private set; }

		public ReadOnlyList<FsslThing5> Thing5s { get; private set; }

		internal Fssl(AssetManager manager, ResourceManager resourceManager, AssetLoader info)
			: base(manager, info.Name) {
			var reader = info.Reader;
			ByteOrder order = ByteOrder.LittleEndian;
			const int adjustment = 0x94;

			reader.RequireMagic(Magic);

			int indicator = reader.ReadInt32();
			if (indicator == 0x01000000)
				order = ByteOrder.BigEndian;
			else if (indicator != 1)
				throw new InvalidDataException();

			reader.Require(1, order);
			reader.Require(1, order);
			reader.Require(0x7C, order);
			reader.Require(checked((int)info.Length - adjustment), order);
			reader.Require(0x0B, order);
			int stringHeaderOffset = reader.ReadInt32(order) + adjustment;
			if (stringHeaderOffset != 0xC8)
				throw new InvalidDataException();
			reader.Require(1, order);
			reader.Require(8, order);
			int stringTotal = reader.ReadInt32(order);
			reader.Require(4, order);
			reader.Require(0, order);
			reader.Require(8, order);
			int thing2Total = reader.ReadInt32(order);
			reader.Require(8, order);
			int unusedThing2Count = reader.ReadInt32(order);
			reader.Require(8, order);
			reader.Require(0, order);
			reader.Require(0x10, order); // Thing3Size?
			int thing3Total = reader.ReadInt32(order);
			reader.Require(4, order); // WeirdThingSize?
			int weirdCount = reader.ReadInt32(order);
			reader.Require(8, order);
			reader.Require(0, order);
			reader.Require(0x3C, order); // Thing4Size?
			int thing4Count = reader.ReadInt32(order);
			reader.Require(0x30, order); // Thing5Size?
			int thing5Total = reader.ReadInt32(order);
			int stringDataOffset = reader.ReadInt32(order) + adjustment;
			reader.Require(0, order);
			reader.Require(stringDataOffset - adjustment, order);
			reader.Require(0x2D0C, order);
			reader.Require(checked((int)info.Length - adjustment), order);
			reader.Require(0, order);
			reader.Require(checked((int)info.Length - adjustment), order);
			reader.Require(0, order);

			// Offset 0x94:
			reader.Require(0, order);
			int thing3Offset = reader.ReadInt32(order) + adjustment;
			int thing3Count = reader.ReadInt32(order);
			int thing2Offset = reader.ReadInt32(order) + adjustment;
			int thing2Count = reader.ReadInt32(order);
			int thing5Offset = reader.ReadInt32(order) + adjustment;
			int thing5Count = reader.ReadInt32(order);
			Guid guid = new Guid(reader.ReadBytes(16));
			reader.Require(stringHeaderOffset - adjustment, order);
			reader.Require(stringTotal, order);

			int unusedThing2Offset = thing2Offset + thing2Total * FsslThing2.DataSize;
			int weirdOffset = thing3Offset + thing3Total * FsslThing3.DataSize;
			int thing4Offset = weirdOffset + weirdCount * FsslWeird.DataSize;

			if (thing2Offset != stringHeaderOffset + stringTotal * 8)
				throw new InvalidDataException();
			if (thing2Offset + (thing2Total + unusedThing2Count) * FsslThing2.DataSize != thing3Offset)
				throw new InvalidDataException();
			if (thing5Offset != thing4Offset + thing4Count * FsslThing4.DataSize)
				throw new InvalidDataException();
			if (stringDataOffset != thing5Offset + thing5Total * FsslThing5.DataSize)
				throw new InvalidDataException();

			var strings = new RichList<string>(stringTotal);
			int[] stringOffsets = reader.ReadArrayInt32(stringTotal * 2, order);
			for (int index = 0; index < stringTotal; index++) {
				reader.BaseStream.Position = stringOffsets[index * 2 + 0] + adjustment;
				strings.Add(reader.ReadString(stringOffsets[index * 2 + 1] * 2 - 2, order == ByteOrder.LittleEndian ? Encoding.Unicode : Encoding.BigEndianUnicode));
			}
			Strings = strings;

			reader.BaseStream.Position = thing2Offset;
			var thing2s = new RichList<FsslThing2>(thing2Count);
			var unusedThing2s = new RichList<FsslThing2>(unusedThing2Count);
			for (int index = 0; index < thing2Count; index++)
				thing2s.Add(new FsslThing2(this, index, reader, order));
			for (int index = 0; index < unusedThing2Count; index++)
				unusedThing2s.Add(new FsslThing2(this, index, reader, order));
			Thing2s = thing2s;
			UnusedThing2s = unusedThing2s;

			if (reader.BaseStream.Position != thing3Offset)
				throw new InvalidOperationException();
			var thing3s = new RichList<FsslThing3>(thing3Count);
			Thing3s = thing3s;
			for (int index = 0; index < thing3Count; index++)
				thing3s.Add(new FsslThing3(this, index, reader, order));

			if (reader.BaseStream.Position != weirdOffset)
				throw new InvalidOperationException();
			var weirds = new RichList<FsslWeird>(weirdCount);
			Weirds = weirds;
			for (int index = 0; index < weirdCount; index++)
				weirds.Add(new FsslWeird(this, index, reader, order));

			if (reader.BaseStream.Position != thing4Offset)
				throw new InvalidOperationException();
			var thing4s = new RichList<FsslThing4>(thing4Count);
			Thing4s = thing4s;
			for (int index = 0; index < thing4Count; index++)
				thing4s.Add(new FsslThing4(this, index, reader, order, unusedThing2Offset, unusedThing2Count, weirdOffset, weirdCount, adjustment));

			if (reader.BaseStream.Position != thing5Offset)
				throw new InvalidOperationException();
			var thing5s = new RichList<FsslThing5>(thing5Count);
			Thing5s = thing5s;
			for (int index = 0; index < thing5Count; index++)
				thing5s.Add(new FsslThing5(this, index, reader, order, thing4Offset, thing4Count, adjustment));

			for(int index = 0; index < thing4s.Count; index++) {
				FsslThing4 item = thing4s[index];
				int offset = item.ThingerOffset;
				int next = index < thing4s.Count - 1 ? thing4s[index + 1].ThingerOffset : checked((int)info.Length);
				reader.BaseStream.Position = offset;
				item.Thingers = new RichList<short>(reader.ReadArrayInt16((next - offset) / 2, order));
			}

			Range
				rangeWeird = Range.Calculate(from i in weirds select i.U1);

			int[] offsets = new int[] { stringHeaderOffset, thing2Offset, unusedThing2Offset, thing3Offset, weirdOffset, thing4Offset, thing5Offset, stringDataOffset, stringOffsets[stringOffsets.Length - 2] + stringOffsets[stringOffsets.Length - 1] * 2 };
			string stringText = "";

			for (int index = 0; index < stringTotal; index++)
				stringText += "{  \"" + strings[index].Trim() + "\" }, // " + index + "\r\n";
		}

		internal static int CheckOffsetCount(int offset, int count, int arrayOffset, int arrayCount, int elementSize, int adjustment) {
			if (offset == -1) {
				if (count != 0)
					throw new InvalidDataException();
				return -1;
			} else {
				int baseOffset = offset + adjustment - arrayOffset;

				if (baseOffset < 0)
					throw new InvalidDataException();
				if (baseOffset >= arrayOffset * arrayCount)
					throw new InvalidDataException();
				if (baseOffset % elementSize != 0)
					throw new InvalidDataException();
				return baseOffset / elementSize;
			}
		}
	}

	public struct Range {
		public readonly int Min, Max;
		public int Span { get { return Max - Min + 1; } }

		public Range(int min, int max) { Min = min; Max = max; }
		public override string ToString() { return string.Format("{0}/{0:X}h to {1}/{1:X}h (span {2}/{2:X}h)", Min, Max, Span); }

		public static Range Calculate(IEnumerable<int> collection) {
			return Calculate(collection, (v) => v);
		}

		public static Range Calculate<T>(IEnumerable<T> collection, Func<T, int> getInt) {
			int min = int.MaxValue;
			int max = int.MinValue;

			foreach (var item in collection) {
				int value = getInt(item);
				min = Math.Min(min, value);
				max = Math.Max(max, value);
			}

			return new Range(min, max);
		}
	}

	public class FsslResource : Asset {
		public Fssl Fssl { get; private set; }

		public int Index { get; private set; }

		public FsslResource(Fssl fssl, int index)
			: base(fssl.Manager, "") {
			Name = GetType().Name + " " + index;
			Fssl = fssl;
			Index = index;
		}
	}

	public class FsslThing2 : FsslResource {
		public const int DataSize = 8;

		public FsslThing2(Fssl fssl, int index, BinaryReader reader, ByteOrder order)
			: base(fssl, index) {
			Unknowns.ReadInt32s(reader, 1, "U1", order);
			Unknowns.ReadInt16s(reader, 2, "U2", order);
		}

		public override string ToString() {
			return Unknowns.ToCommaSeparatedList();
		}
	}

	public class FsslThing3 : FsslResource {
		public const int DataSize = 16;

		public FsslThing3(Fssl fssl, int index, BinaryReader reader, ByteOrder order)
			: base(fssl, index) {
			Unknowns.ReadInt32s(reader, 1, "U1", order);
			reader.Require(-1, order);
			reader.Require(0, order);
			Unknowns.ReadInt16s(reader, 2, "U2", order);
		}

		public override string ToString() {
			return Unknowns.ToCommaSeparatedList();
		}
	}

	public class FsslWeird : FsslResource {
		public const int DataSize = 4;

		public int U1 { get; private set; }

		public FsslWeird(Fssl fssl, int index, BinaryReader reader, ByteOrder order)
			: base(fssl, index) {
			U1 = reader.ReadUInt16(order);
			reader.Require((short)0x01FF, order);
		}

		public override string ToString() {
			return string.Format("{0:X}h", U1);
		}
	}

	public class FsslThing4 : FsslResource {
		public const int DataSize = 0x3C;

		/// <summary>Zero-based index of this <see cref="FsslThing4"/> in its <see cref="FsslThing5"/>.</summary>
		public int Thing5Index { get; private set; }

		internal int ThingerOffset { get; private set; }

		public ReadOnlyList<short> Thingers { get; internal set; }

		public ReadOnlyList<ReadOnlyList<FsslWeird>> Weirds { get; internal set; }

		public int UnusedThing2Start { get; private set; }
		public int UnusedThing2Count { get; private set; }

		internal FsslThing4(Fssl fssl, int index, BinaryReader reader, ByteOrder order, int unusedThing2Offset, int unusedThing2Count, int weirdOffset, int weirdCount, int adjustment)
			: base(fssl, index) {
			Thing5Index = reader.ReadInt32(order);
			ThingerOffset = reader.ReadInt32(order);
			reader.Require(1, order); // 3

			RichList<ReadOnlyList<FsslWeird>> weirds = new RichList<ReadOnlyList<FsslWeird>>(4);
			Weirds = weirds;
			for (int i = 0; i < 4; i++) {
				int offset = reader.ReadInt32(order);
				int count = reader.ReadInt32(order);
				int start = Fssl.CheckOffsetCount(offset, count, weirdOffset, weirdCount, FsslWeird.DataSize, adjustment);
				RichList<FsslWeird> list = new RichList<FsslWeird>(count);
				for (int weirdIndex = 0; weirdIndex < count; weirdIndex++)
					list.Add(Fssl.Weirds[start + weirdIndex]);
				weirds.Add(list);
			}

			int thing2Offset = reader.ReadInt32(order);
			UnusedThing2Count = reader.ReadInt32(order);
			UnusedThing2Start = Fssl.CheckOffsetCount(thing2Offset, UnusedThing2Count, unusedThing2Offset, unusedThing2Count, FsslThing2.DataSize, adjustment);

			reader.Require(-1, order); // u14
			reader.Require(0, order); // u15
		}

		public override string ToString() {
			string weirds = "(" + string.Join(", ", from list in Weirds select list.Count == 0 ? "null" : "[" + string.Join(", ", from weird in list select weird.U1) + "]") + ")";

			return string.Format("Thingers: {0}, UnusedThing2s: {1}+{2}, Weirds: {3}{4}", string.Join(" ", Thingers), UnusedThing2Start, UnusedThing2Count, weirds, Unknowns.ToCommaPrefixedList());
		}
	}

	public class FsslThing5 : FsslResource {
		public const int DataSize = 0x30;

		public int Thing4Start { get; private set; }
		public IEnumerable<FsslThing4> Thing4 { get { for (int index = 0; index < Thing4Count; index++) yield return Fssl.Thing4s[Thing4Start + index]; } }
		public int Thing4Count { get; private set; }

		internal FsslThing5(Fssl fssl, int index, BinaryReader reader, ByteOrder order, int thing4sOffset, int thing4sCount, int adjustment)
			: base(fssl, index) {
			Unknowns.ReadInt32s(reader, 1, "U1", order);
			Unknowns.ReadInt16s(reader, 2, "U2", order);
			Unknowns.ReadInt32s(reader, 8, "U3", order);

			int thing4Offset = reader.ReadInt32(order) + adjustment;
			if (thing4Offset < thing4sOffset)
				throw new InvalidDataException();
			if (thing4Offset >= thing4sOffset + thing4sCount * FsslThing4.DataSize)
				throw new InvalidDataException();
			if ((thing4Offset - thing4sOffset) % FsslThing4.DataSize != 0)
				throw new InvalidDataException();
			Thing4Start = (thing4Offset - thing4sOffset) / FsslThing4.DataSize;
			Thing4Count = reader.ReadInt32(order);
		}

		public override string ToString() {
			return string.Format("Thing4:{0}+{1}{2}", Thing4Start, Thing4Count, Unknowns.ToCommaPrefixedList());
		}
	}

	public class FsslFormat : AssetFormat {
		public FsslFormat(Engine engine) : base(engine, typeof(Fssl), canLoad: true) { }

		public override LoadMatchStrength LoadMatch(AssetLoader context) {
			return context.Reader.MatchMagic(Fssl.Magic) ? LoadMatchStrength.Medium : LoadMatchStrength.None;
		}

		public override Asset Load(AssetLoader context) {
			return new Fssl(Manager, ResourceManager, context);
		}
	}
}
