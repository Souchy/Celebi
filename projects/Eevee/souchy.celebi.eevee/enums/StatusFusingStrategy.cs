using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums
{
    /*
     * Replace Values:
     *      - Always
     *      - Never
     *      - Keep best
     *      - Keep worst
     * 
     * Refresh duration:
     *      - Yes
     *      - No
     * 
     * Affect which instance:
     *      - All
     *      - Oldest duration
     *      - Newest duration
     *      - Best values (???????? how would I do that if the merging code is generic for any effect)
     *      - Worst values
     * 
     * Condition If:
     *      - Max Stacks
     *      - No stacks
     *      - Always
     * 
     */

    /// <summary>
    /// 2 options when at max stacks:
    ///     1 - refresh the duration of the oldest instance
    ///     2 - replace the values of the worst instance
    ///     
    /// 
    /// </summary>
    public enum StatusFusingStrategy
    {
        /// <summary>
        /// Dont do anything at all, reject new status instances until the current ones are gone
        /// </summary>
        NoFusing,
        /// <summary>
        /// Replace the instance with the lowest duration remaining 
        /// </summary>
        RefreshOldest_KeepBestValue_IfMaxStacks,
        /// <summary>
        /// 
        /// </summary>
        RefreshNewest_KeepBestValue_IfMaxStacks,

        RefreshAll_Always,

        RefreshDuration_IfMaxStacks,
        ReplaceValueIfBetter_IfMaxStacks,
        ReplaceValueAlways_IfMaxStacks,

        RefreshDuration_AllStacks,
        
    }
}
