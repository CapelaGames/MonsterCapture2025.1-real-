using UnityEngine;
using System.Collections.Generic;

public class CarShowroom : MonoBehaviour
{
    void Start()
    {
        Car car = new Clown();

        (car as Clown)?.EjectClown();

        Clown clownCar = new Clown();
        clownCar.SetOwner("Judah");

        F1 F1Car = new F1();
        F1Car.SetOwner("Andrew");

        List<Car> cars = new List<Car>();
        cars.Add(F1Car);    //0
        cars.Add(clownCar); //1
        cars.Add(car);      //2

        int i = Random.Range(0, cars.Count);
        Car raceCar1 = cars[i];
        cars.RemoveAt(i);
        i = Random.Range(0, cars.Count);
        Race(raceCar1, cars[i]);

    }
    void Race(Car car1, Car car2)
    {
        if(car1.speed > car2.speed)
        {
            Debug.Log("HERE IS YOUR WINNER " + car1.VictorySpeech());
        }
        else
        {
            Debug.Log("HERE IS YOUR WINNER " + car2.VictorySpeech());
        }
    }
}
