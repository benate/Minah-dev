using UnityEngine;
using System.Collections;

public class Npc : PerformActor
{
	public GameObject modelObject;
	public float startPosZ;

	new public void Awake()
	{
		GameObject go = GameObject.Instantiate(modelObject) as GameObject;
		go.transform.parent = transform;
		go.transform.localPosition = new Vector3(0.0f, 0.0f, startPosZ);

		anim = gameObject.AddComponent<ActorAnimated>();
		translater = gameObject.AddComponent<Translater>();

		OnAwake();
	}

	public override void FsmEvent(FsmUnit unit, Fsm.Event fsmEvent = Fsm.Event.None)
	{
		fsm.ChangeState(Game.FsmType.Hover);
	}

	protected override void OnStart()
	{
		base.OnStart();

		fsm.AddState(new FsmUnit_Hover(fsm, "Walk"));
		fsm.ChangeState(Game.FsmType.Hover);
	}
}
