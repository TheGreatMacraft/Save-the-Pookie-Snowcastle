using System.Collections.Generic;

public sealed class SelectedClass
    : Scalar<Class>
{
    private readonly ClassType selectedType;
    private readonly PlayerState playerState;
    private readonly Togglable hologram;

    private List<Class> selectedClass = new(1);

    
    public SelectedClass(
        ClassType selectedType,
        PlayerState playerState,
        Togglable hologram
    )
    {
        this.selectedType = selectedType;
        this.playerState = playerState;
        this.hologram = hologram;
    }

    public Class Value()
    {
        if (selectedClass.Count == 0)
        {
            switch (selectedType)
            {
                case ClassType.Flanker:
                    selectedClass.Add(
                        new FlankerClass()
                    );
                    break;
            
                case ClassType.Engineer:
                    selectedClass.Add(
                        new EngineerClass(hologram, playerState)
                    );
                    break;
            }
        }
        
        return selectedClass[0];
    }
}