using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TestingInterfaceCovariance;
public class TestingInterfaceCovarianceMono : MonoBehaviour
{

    [ContextMenuItem("LogPeople", "LogPeople")]
    public string[] m_people;

    public void LogPeople()
    {
        int length = m_people.Length;
        Person[] people = new Person[length];
        for (int i = 0; i < length; i++)
        {
            people[i] = new Person(m_people[i]);
        }
        TestingInterfaceCovariance.Test(people);
    }

}
