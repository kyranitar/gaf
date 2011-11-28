using UnityEngine;
using System.Collections;

public class SocialUser {

  private string firstName;
  public string FirstName {
    get { return firstName; }
  }

  private string lastName;
  public string LastName {
    get { return lastName; }
  }

  private Texture profileImage;
  public Texture ProfileImage {
    get { return profileImage; }
  }

  private int experiencePoints;
  public int ExperiencePoints {
    get { return experiencePoints; }
  }

  public SocialUser() {
    // TODO Fill the above fields.

    this.firstName = "";
    this.lastName = "";
    this.profileImage = null;
  }

  public void ModifyExperiencePoints(int amount) {
    // TODO Save to database.

    this.experiencePoints += amount;
  }

}
