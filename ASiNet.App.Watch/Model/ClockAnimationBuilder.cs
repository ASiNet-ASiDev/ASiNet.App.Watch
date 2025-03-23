using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASiNet.App.Watch.Model;
public class ClockAnimationBuilder
{


    public ClockAnimationBuilder AddActiveSegments(params int[] segments)
    {

        return this;
    }

    public ClockAnimationBuilder AddDelay(int milliseconds)
    {

        return this;
    }

    public ClockAnimationBuilder AddInactiveSegments(params int[] segments)
    {

        return this;
    }


}
