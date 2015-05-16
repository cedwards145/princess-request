using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD30
{
    public class Messages
    {
        public static int storyMarker = -4;
        private static Game1 gameRef;

        public static void load(Game1 game)
        {
            gameRef = game;
        }

        public static void getStoryMessage()
        {
            Queue<string> messageQueue = new Queue<string>();
            if (storyMarker == -4)
            {
                messageQueue.Enqueue("Oh dear, this is bad. This is very bad.");
                gameRef.follower.message("WASD to move, ENTER to interact!");
            }
            else if (storyMarker == -3)
            {
                messageQueue.Enqueue("I'm going to die.");
            }
            else if (storyMarker == -2)
            {
                messageQueue.Enqueue("I accidentally turned off the Machine.");
            }
            else if (storyMarker == -1)
            {
                messageQueue.Enqueue("I had completely forgotten that it was the only thing...");
                messageQueue.Enqueue("... keeping me alive.");
            }
            else if (storyMarker == 0)
            {
                messageQueue.Enqueue("How silly of me.");
            }
            else if (storyMarker == 1)
            {                
                messageQueue.Enqueue("Ah, you there! Yes, you! Would you help a dying young...");
                messageQueue.Enqueue("... Princess like me? I would be eternally greatful!");
                messageQueue.Enqueue("Step into that teleporter there, and you'll be...");
                messageQueue.Enqueue("... taken to an amazing new world! You can explore...");
                messageQueue.Enqueue("... it as much as you want, as long as you flip one...");
                messageQueue.Enqueue("... teeny tiny switch for me.");
                messageQueue.Enqueue("You'll do it? Excellent! I will be eternally in your...");
                messageQueue.Enqueue("... debt!");
                messageQueue.Enqueue("This little wisp will accompany you on your journey.");
                messageQueue.Enqueue("As long as you are near her, I will know where you are.");
                messageQueue.Enqueue("And it will always find you. No matter where you go.");
                messageQueue.Enqueue("...");
                messageQueue.Enqueue("...");
                messageQueue.Enqueue("So I can give you help at any moment!");
                messageQueue.Enqueue("Well, off you go! You have a quest to complete!");
                messageQueue.Enqueue("Step into the teleported behind me.");
            }
            else if (storyMarker == 2)
            {
                messageQueue.Enqueue("Somewhere in this world is a switch to start the machine.");
                messageQueue.Enqueue("You MUST find it!");
                messageQueue.Enqueue("Once you've turned it on, you can use the teleporter to...");
                messageQueue.Enqueue("... return to me.");
                messageQueue.Enqueue("Oh, and don't mind anything you find lying around.");
                messageQueue.Enqueue("It's been a long time since I've been here and I may not...");
                messageQueue.Enqueue("... have cleaned up after myself properly.");
                messageQueue.Enqueue("No need to worry though. There's DEFINITELY nothing...");
                messageQueue.Enqueue("... alive lurking around here. That's for sure.");
            }
            else if (storyMarker == 3)
            {
                messageQueue.Enqueue("Hmm, this could be it. You may very well have just...");
                messageQueue.Enqueue("... saved us all! Let's find out.");
            }
            else if (storyMarker == 4)
            {
                messageQueue.Enqueue("Regretably, that switch did not turn on the Machine.");
                messageQueue.Enqueue("It might have opened up a door for you though.");
                messageQueue.Enqueue("Have another look around.");
            }
            else if (storyMarker == 5)
            {
                messageQueue.Enqueue("This is the control room for this world!");
                messageQueue.Enqueue("You've done it! You've saved us all!");
                messageQueue.Enqueue("Pull that lever and head on home.");
                messageQueue.Enqueue("I feel silly for panicking.");
            }

            else if (storyMarker == 6)
            {
                messageQueue.Enqueue("...");
                messageQueue.Enqueue("......");
                messageQueue.Enqueue("Unfortunately, that didn't do it either.");
                messageQueue.Enqueue("Back to panicking!");
            }
            else if (storyMarker == 7)
            {
                messageQueue.Enqueue("I know I've said it before, but THIS time...");
                messageQueue.Enqueue("... this will work!");
                messageQueue.Enqueue("This is the belly of the Machine, this switch...");
                messageQueue.Enqueue("... must be the one!");
            }
            else if (storyMarker == 8)
            {
                messageQueue.Enqueue("...");
            }
            else if (storyMarker == 9)
            {
                messageQueue.Enqueue("I can't believe I missed it! That switch?");
                messageQueue.Enqueue("It was THAT switch?");
                messageQueue.Enqueue("Ah well. At least the Machine is running again.");
                gameRef.setRespawn();
                messageQueue.Enqueue("Head on back. I've got plenty more jobs for you.");
            }
            else if (storyMarker == 10)
            {
                messageQueue.Enqueue("I really do owe you my thanks. You've done me...");
                messageQueue.Enqueue("... more of a favour than you know.");
                messageQueue.Enqueue("I wonder if you would have helped me if you...");
                messageQueue.Enqueue("... knew what that machine really did.");
                messageQueue.Enqueue("...");
                messageQueue.Enqueue("Probably not.");
                messageQueue.Enqueue("Now, isn't it fitting that after saving a...");
                messageQueue.Enqueue("... princess, the valiant knight should be...");
                messageQueue.Enqueue("... allowed to stay with her FOREVER?");
                messageQueue.Enqueue("I'm fine with that.");

                gameRef.player.controllable = false;
                gameRef.player.setPosition(new Microsoft.Xna.Framework.Vector2(650, 200));
                gameRef.follower.message("Alright! Let's go!");
                gameRef.currentMapState.addEntity(new Tongue(gameRef, null, new Microsoft.Xna.Framework.Vector2(500, 100)));
            }

            storyMarker++;

            if (messageQueue.Count > 0)
                gameRef.sendMessage(messageQueue, gameRef.currentMapState);
        }
    }
}
