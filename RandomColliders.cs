using Godot;
using System;

public class RandomColliders : Node2D
{
	public override void _Ready()
	{
		for (int i = 0; i < 10; ++i) {
			Vector2 offset = new Vector2(0, -256);
			offset += Random2D.PointInCircle(0.0f, 256.0f);
			SpawnRandomBody(offset);
		}
	}

	public void SpawnRandomBody(Vector2 offset)
	{
		RigidBody2D body = new RigidBody2D();
		ConvexPolygonShape2D shape = new ConvexPolygonShape2D();
		shape.Points = GetRandomPolygon();
		Physics2DServer.BodyAddShape(body.GetRid(), shape.GetRid());
		VisualShape2D visual = new VisualShape2D();
		visual.Shape = shape;
		visual.Modulate = Godot.Random.Color;
		body.AddChild(visual);
		AddChild(body);
		body.Position = offset;
	}

	public Vector2[] GetRandomPolygon()
	{
		int sides = (int)Godot.Random.Range(3, 10);
		Vector2[] polygon = GoostGeometry2D.RegularPolygon(sides, 64.0f);
		for (int i = 0; i < polygon.Length; ++i) {
			polygon[i] += Random2D.PointInCircle(5.0f, 20.0f);
		}
		return polygon;
	}
}
