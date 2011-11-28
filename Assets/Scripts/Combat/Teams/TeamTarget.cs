using UnityEngine;
using System.Collections;

/// Used by weapons to determine which teams they are targeting.
/// For basic two-way battles, use the TargetJust method.
/// 
/// Author: Timothy Jones
public class TeamTarget {

  public delegate bool Condition(CombatTeam team);

  private Condition condition;

  public TeamTarget(Condition condition) {
    this.condition = condition;
  }

  /// Whether the weapon is targeting the given team.
  public bool IsTargeting(CombatTeam team) {
    return condition(team);
  }

  /// Helper for determining if a game object is targeted.
  public bool IsTargeting(GameObject gameObject) {
    TeamMarker marker = gameObject.GetComponent<TeamMarker>();
    if (marker != null) {
      return IsTargeting(marker.Team);
    }

    return false;
  }

  /// Finds the nearest target to the given position.
  /// Returns null if no such target exists.
  public GameObject FindNearestTarget(Vector3 to) {
    float minimumDistance = float.PositiveInfinity;
    GameObject result = null;

    // Loop over every ship, checking if this missile is targeting their team.
    foreach (GameObject ship in TeamMarker.GetAllMarkedGameObjects()) {
      CombatTeam team = ship.GetComponent<TeamMarker>().Team;
      if (IsTargeting(team)) {
        float distance = Vector3.Distance(ship.transform.position, to);

        // This ship is closer than the previously closest ship.
        if (distance < minimumDistance) {
          result = ship;
          minimumDistance = distance;
        }
      }
    }

    return result;
  }

  /// Targets all teams indiscriminately.
  public static TeamTarget TargetAll() {
    return new TeamTarget(delegate(CombatTeam team) {
      return true;
    });
  }

  /// Targets only the given team.
  /// More effecient than the Target method.
  public static TeamTarget TargetJust(CombatTeam target) {
    return new TeamTarget(delegate(CombatTeam team) {
      return target == team;
    });
  }

  /// Targets the given teams.
  public static TeamTarget Target(params CombatTeam[] targets) {
    return new TeamTarget(delegate(CombatTeam team) {
      foreach (CombatTeam target in targets) {
        if (target == team) {
          return true;
        }
      }

      return false;
    });
  }

  /// Targets every team except the given ones.
  public static TeamTarget TargetAllExcept(params CombatTeam[] targets) {
    return new TeamTarget(delegate(CombatTeam team) {
      foreach (CombatTeam target in targets) {
        if (target == team) {
          return false;
        }
      }

      return true;
    });
  }

}
