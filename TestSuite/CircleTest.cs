﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using remonduk;

namespace TestSuite
{
	[TestClass]
	public class CircleTest
	{
		Constants constants = Constants.Instance;

		int PRECISION = 6;
		double EPSILON = .0001;
		[TestMethod]
		public void CircleTestXYR()
		{
			// check that all other fields are set correctly
			Circle circle = new Circle(2, 3, 5);

			AreEqual(2, circle.x);
			AreEqual(3, circle.y);
			AreEqual(5, circle.r);
			AreEqual(Circle.MASS, circle.mass);

			AreEqual(Circle.VELOCITY, circle.velocity);
			AreEqual(Circle.VELOCITY_ANGLE, circle.velocity_angle);
			AreEqual(Circle.VELOCITY * Math.Cos(Circle.VELOCITY_ANGLE), circle.vx);
			AreEqual(Circle.VELOCITY * Math.Sin(Circle.VELOCITY_ANGLE), circle.vy);

			AreEqual(Circle.ACCELERATION, circle.acceleration);
			AreEqual(Circle.ACCELERATION_ANGLE, circle.acceleration_angle);
			AreEqual(Circle.ACCELERATION * Math.Cos(Circle.ACCELERATION_ANGLE), circle.ax);
			AreEqual(Circle.ACCELERATION * Math.Sin(Circle.ACCELERATION_ANGLE), circle.ay);

			AreEqual(null, circle.target);
			AreEqual(0F, circle.min_dist);
			AreEqual(0F, circle.max_dist);

			circle = new Circle(2, 3, 5, 8F);
			AreEqual(8F, circle.mass);
		}

		[TestMethod]
		public void CircleTestXYRV() {
			Circle circle = new Circle(2, 3, 5, 8, Math.PI / 4);
			
			AreEqual(2, circle.x);
			AreEqual(3, circle.y);
			AreEqual(5, circle.r);
			AreEqual(Circle.MASS, circle.mass);

			AreEqual(8F, circle.velocity);
			AreEqual(Math.PI / 4, circle.velocity_angle);
			AreEqual(4 * Math.Sqrt(2), circle.vx);
			AreEqual(4 * Math.Sqrt(2), circle.vy);

			AreEqual(Circle.ACCELERATION, circle.acceleration);
			AreEqual(Circle.ACCELERATION_ANGLE, circle.acceleration_angle);
			AreEqual(Circle.ACCELERATION * Math.Cos(Circle.ACCELERATION_ANGLE), circle.ax);
			AreEqual(Circle.ACCELERATION * Math.Sin(Circle.ACCELERATION_ANGLE), circle.ay);

			AreEqual(null, circle.target);
			AreEqual(0F, circle.min_dist);
			AreEqual(0F, circle.max_dist);

			circle = new Circle(2, 3, 5, 8, Math.PI / 4, 13F);
			AreEqual(13F, circle.mass);
		}
		
		[TestMethod]
		public void CircleTestXYRVA() {
			Circle circle = new Circle(2, 3, 5, 8, Math.PI / 4, 13, 1 * Math.PI / 4);

			AreEqual(2, circle.x);
			AreEqual(3, circle.y);
			AreEqual(5, circle.r);
			AreEqual(Circle.MASS, circle.mass);

			AreEqual(8F, circle.velocity);
			AreEqual(Math.PI / 4, circle.velocity_angle);
			AreEqual(4 * Math.Sqrt(2), circle.vx);
			AreEqual(4 * Math.Sqrt(2), circle.vy);

			AreEqual(13, circle.acceleration);
			AreEqual(Math.PI / 4, circle.acceleration_angle);
			AreEqual(6.5 * Math.Sqrt(2), circle.ax);
			AreEqual(6.5 * Math.Sqrt(2), circle.ay);

			AreEqual(null, circle.target);
			AreEqual(0F, circle.min_dist);
			AreEqual(0F, circle.max_dist);

			circle = new Circle(2, 3, 5, 8, Math.PI / 4, 13, 1 * Math.PI / 4, 21F);
			AreEqual(21F, circle.mass);
		}

		[TestMethod]
		public void setRadiusTest() {
			Circle circle = new Circle(1, 1, 2);
			circle.setRadius(10F);
			AreEqual(10F, circle.r);
			circle.setRadius(1F);
			AreEqual(1F, circle.r);

			try {
				circle.setRadius(.99F);
				Assert.Fail();
			}
			catch (ArgumentException e) {
				AreEqual("radius: 0.99", e.Message);
			}
			try {
				circle.setRadius(0F);
				Assert.Fail();
			}
			catch (ArgumentException e) {
				AreEqual("radius: 0", e.Message);
			}
			try {
				circle.setRadius(-1.0F);
				Assert.Fail();
			}
			catch (ArgumentException e) {
				AreEqual("radius: -1", e.Message);
			}
		}

		[TestMethod]
		public void setMassTest() {
			Circle circle = new Circle(1, 1, 2);
			circle.setMass(10F);
			AreEqual(10F, circle.mass);
			circle.setMass(0F);
			AreEqual(0F, circle.mass);
			circle.setMass(-1F);
			AreEqual(-1F, circle.mass);
		}

		[TestMethod]
		public void setVelocityTest() {
			Circle circle = new Circle(1, 1, 2);
			circle.setVelocity(10, 0);
			AreEqual(10F, circle.velocity);
			AreEqual(0, circle.velocity_angle);
			AreEqual(10, circle.vx);
			AreEqual(0, circle.vy);

			circle.setVelocity(20, Math.PI / 2);
			AreEqual(20F, circle.velocity);
			AreEqual(Math.PI / 2, circle.velocity_angle);
			AreEqual(0, circle.vx);
			AreEqual(20, circle.vy);

			circle.setVelocity(30, Math.PI / 4);
			AreEqual(30F, circle.velocity);
			AreEqual(Math.PI / 4, circle.velocity_angle);
			AreEqual(15 * Math.Sqrt(2), circle.vx);
			AreEqual(15 * Math.Sqrt(2), circle.vy);

			circle.setVelocity(40, 5 * Math.PI / 4);
			AreEqual(40F, circle.velocity);
			AreEqual(5 * Math.PI / 4, circle.velocity_angle);
			AreEqual(-20 * Math.Sqrt(2), circle.vx);
			AreEqual(-20 * Math.Sqrt(2), circle.vy);
		}

		[TestMethod]
		public void setAccelerationTest() {
			Circle circle = new Circle(1, 1, 2);
			circle.setAcceleration(10, 0);
			AreEqual(10F, circle.acceleration);
			AreEqual(0, circle.acceleration_angle);
			AreEqual(10, circle.ax);
			AreEqual(0, circle.ay);

			circle.setAcceleration(20, Math.PI / 2);
			AreEqual(20F, circle.acceleration);
			AreEqual(Math.PI / 2, circle.acceleration_angle);
			AreEqual(0, circle.ax);
			AreEqual(20, circle.ay);

			circle.setAcceleration(30, Math.PI / 4);
			AreEqual(30F, circle.acceleration);
			AreEqual(Math.PI / 4, circle.acceleration_angle);
			AreEqual(15 * Math.Sqrt(2), circle.ax);
			AreEqual(15 * Math.Sqrt(2), circle.ay);

			circle.setAcceleration(40, 5 * Math.PI / 4);
			AreEqual(40F, circle.acceleration);
			AreEqual(5 * Math.PI / 4, circle.acceleration_angle);
			AreEqual(-20 * Math.Sqrt(2), circle.ax);
			AreEqual(-20 * Math.Sqrt(2), circle.ay);
		}

		[TestMethod]
		public void followTestT() {
			Circle leader = new Circle(1, 1, 2);
			Circle sheep = new Circle(3, 5, 8);

			sheep.follow(leader);
			AreEqual(null, leader.target);
			AreEqual(0F, leader.min_dist);
			AreEqual(0F, leader.max_dist);

			AreEqual(leader, sheep.target);
			AreEqual(10F, sheep.min_dist);
			AreEqual(10F, sheep.max_dist);

			sheep.follow();
			AreEqual(null, leader.target);
			AreEqual(0F, leader.min_dist);
			AreEqual(0F, leader.max_dist);

			AreEqual(null, sheep.target);
			AreEqual(0F, sheep.min_dist);
			AreEqual(0F, sheep.max_dist);
		}

		[TestMethod]
		public void followTestTMM() {
			Circle leader = new Circle(1, 1, 2);
			Circle sheep = new Circle(3, 5, 8);

			sheep.follow(leader, 13, 21);
			AreEqual(null, leader.target);
			AreEqual(0F, leader.min_dist);
			AreEqual(0F, leader.max_dist);

			AreEqual(leader, sheep.target);
			AreEqual(13F, sheep.min_dist);
			AreEqual(21F, sheep.max_dist);

			sheep.follow(null, 34, 55);
			AreEqual(null, leader.target);
			AreEqual(0F, leader.min_dist);
			AreEqual(0F, leader.max_dist);

			AreEqual(null, sheep.target);
			AreEqual(0F, sheep.min_dist);
			AreEqual(0F, sheep.max_dist);
		}	

		[TestMethod]
		public void updateAccelerationTest() {
			Circle circle = new Circle(1, 1, 2);
			HashSet<Circle> circles = new HashSet<Circle>();
			
			circle.updateAcceleration(-1 * constants.GRAVITY, constants.GRAVITY_ANGLE);
			AreEqual(constants.GRAVITY, circle.acceleration);
			if (constants.GRAVITY_ANGLE + Math.PI > 2 * Math.PI) {
				AreEqual(constants.GRAVITY_ANGLE - Math.PI, circle.acceleration_angle);
			}
			else {
				AreEqual(constants.GRAVITY_ANGLE + Math.PI, circle.acceleration_angle);
			}
			AreEqual(0, circle.ax);
			AreEqual(-1 * constants.GRAVITY, circle.ay);

			double expected_ax = -1 * constants.GRAVITY * Math.Sqrt(2) / 2;
			double expected_ay = -1 * constants.GRAVITY * Math.Sqrt(2) / 2 - constants.GRAVITY;
			circle.updateAcceleration(-1 * constants.GRAVITY, Math.PI / 4);
			AreEqual(circle.magnitude((float)expected_ax, (float)expected_ay), circle.acceleration);
			AreEqual(circle.angle((float)expected_ay, (float)expected_ax), circle.acceleration_angle);
			AreEqual(expected_ax, circle.ax);
			AreEqual(expected_ay, circle.ay);
		}

		[TestMethod]
		public void updateVelocity() {
			Circle circle = new Circle(1, 1, 2);
			HashSet<Circle> circles = new HashSet<Circle>();

			float velocity = Circle.VELOCITY;
			double velocity_angle = Circle.VELOCITY_ANGLE;
			double vx = Circle.VELOCITY * Math.Cos(Circle.VELOCITY_ANGLE);
			double vy = Circle.VELOCITY * Math.Sin(Circle.VELOCITY_ANGLE);

			for (int i = 0; i < 10; i++) {
				AreEqual(velocity, circle.velocity);
				AreEqual(velocity_angle, circle.velocity_angle);
				AreEqual(vx, circle.vx);
				AreEqual(vy, circle.vy);
				circle.update(circles);
			}

			float gravity = constants.GRAVITY;
			double gravity_angle = Math.PI / 4;
			double gx = gravity * Math.Cos(gravity_angle);
			double gy = gravity * Math.Sin(gravity_angle);

			circle.updateAcceleration(gravity, gravity_angle);
			for (int i = 1; i <= 10; i++) {
				circle.update(circles);
				AreEqual(i * (velocity + gravity), circle.velocity);
				AreEqual(circle.angle((float)(vy + gy), (float)(vx + gx)), circle.velocity_angle);
				AreEqual(i * (vx + gx), circle.vx);
				AreEqual(i * (vy + gy), circle.vy);
			}

			gravity *= -1;
			gx = gravity * Math.Cos(gravity_angle);
			gy = gravity * Math.Sin(gravity_angle);

			circle.updateAcceleration(gravity, gravity_angle);
			for (int i = 10; i > 0; i--) {
				AreEqual(i * (velocity - gravity), circle.velocity);
				//AreEqual();
				//AreEqual(i * (vx + gx), circle.vx);
				//AreEqual(i * (vy + gy), circle.vy);
				circle.update(circles);
			}
		}

		[TestMethod]
		public void updatePosition() {
			// need to check position, velocity, and acceleration
		}

		[TestMethod]
		public void collideTest() {

		}

		[TestMethod]

		public void moveTest() {

		}

		[TestMethod]
		public void updateTest() {

		}

		[TestMethod]
		public void drawTest() {
			// is this even testable?
		}

		[TestMethod]
		public void distanceTest() {
			// gonna have to make the method public to do this
		}

		//private void AreEqual(double expected, double actual) {
		//	Assert.AreEqual(expected, actual);
		//}

		private void AreEqual(double expected, double actual) {
			System.Diagnostics.Debug.WriteLine("expected: " + expected + " actual: " + actual);
			AreEqual(true, Math.Abs(expected - actual) < EPSILON);
		}

		private void AreEqual(bool expected, bool actual) {
			Assert.AreEqual(expected, actual);
		}

		private void AreEqual(Object expected, Object actual) {
			Assert.AreEqual(expected, actual);
		}

		//private void AreEqual(String expected, String actual) {
		//	Assert.AreEqual(expected, actual);
		//}

		private double round(double value) {
			return Math.Round(value, PRECISION);
		}
	}
}