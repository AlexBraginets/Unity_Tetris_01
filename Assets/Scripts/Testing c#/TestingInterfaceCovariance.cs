using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestingInterfaceCovariance
{
    public static void Test(params Person[] people)
    {
        var peopleList = new List<Person>();
        peopleList.AddRange(people);
        IEnumerable<Person> peopleIEnum = peopleList;
        IEnumerable<object> objects = peopleIEnum;

        LogPeople(objects);
    }
    private static void LogPeople(IEnumerable<object> people)
    {
        foreach(var person in people)
        {
            Debug.Log(person);
        }
    }
    public class Person
    {
        public Person(string name)
        {
            this.name = name;
        }
        private string name;
        public override string ToString()
        {
            return name;
        }
    }
}
