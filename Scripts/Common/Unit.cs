using System.Collections;

public abstract class Unit<Owner>
{
	public Owner owner { private set; get; }

	private Unit() { }
	public Unit(Owner owner) { this.owner = owner; }

	public abstract void FocusIn();
	public abstract void FocusOut();
	public abstract void OnUpdate();
}