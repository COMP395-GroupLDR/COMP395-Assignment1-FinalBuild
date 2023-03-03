/*  Filename:           Customer.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        March 3, 2023
 *  Description:        Refractor from first build script by Han Bi
 *  Revision History:   March 3, 2023 (Yuk Yee Wong): Initial script.
 */

public class Customer
{
    public int id;
    public float interarrivalTime;
    public float serviceTime;

    public Customer(int id, float interarrivalTime, float serviceTime)
    {
        this.id = id;
        this.interarrivalTime = interarrivalTime;
        this.serviceTime = serviceTime;
    }
}
