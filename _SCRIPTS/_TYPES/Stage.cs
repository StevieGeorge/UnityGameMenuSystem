using System.Collections;
using System.Collections.Generic;
using System.Linq;


/*
    Stage will refer the gameplay boundaries and positioning of game elements.
    The word "Level" is way too vague
    Unfortunately "Stage" can also have multiple meanings in the context of games.
    However the same is true of every intuitive alternative ("Map", "Scene", "Field", etc)
    And Stage is more clear than Level.
*/

public class Stage
{
    //dimensions serves two purposes: hold the measurements of each dimension and also the number of them as Count
    public List<int> dimensions = new List<int>();
    public List<Space> spaces = new List<Space>();

    public Stage()
    {
    }

    public Stage(int x, int y)
    {
        for (int i = 0; i < x * y ; i++)
        {
            spaces.Add(new Space(i % x, i / y));
        }
    }


    /* save n-dimensional tetris stages for later
    public Stage(List<int> dims)
    {
        dimensions = dims;
        int depth = dims.Count;
        total = dims.Sum();
        for (int s = 0; s < total; s++)
        {
            Space space = new Space();
            space.coords = new List<int>{s %};
            spaces.Add(new Space());
        }
    }
    */
}



public class Space
{
    public int dimensions = 0;
    public List<int> coords = new List<int>();

    public Space(int x, int y)
    {
        dimensions = 2;
        coords.Add(x);  
        coords.Add(y);  
    }
    public Space(int dims, List<int> co)
    {
        dimensions = dims;
        coords = co;
    }

    public Space()
    {

    }
}
