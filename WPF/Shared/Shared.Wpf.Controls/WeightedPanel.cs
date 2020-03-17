using System.Windows;
using System.Windows.Controls;

namespace Shared.Wpf.Controls
{
	/// <summary>
	/// Das Beispiel ist von Pavan Podila und Kevin Hoffmann aus dem Buch "WPF Control Development" (Kapitel 4)
	/// 
	/// Es zeigt ein Panel mit der Möglichkeit die Breite der Controls innherhalb des Panels über eine Gewichtung zu berechnen.
	/// 
	/// Die Height-Property ist eine AttachedProperty: Diese Property wird nicht über das Panel zugewiesen,
	/// sondern von den Controls, die innerhalb des Panels liegen. Dabei wird die Gewichtung eines jeden Controls ausgelesen und anhand dieser Gewichtung wird die Breite ermittelt.
	/// 
	/// In diesem Artikel wird der Unterschied zwischen Attached Properties, Attached Behavior und Behavior (Blend) erklärt: <seealso cref="https://jasper-net.blogspot.com/2012/12/attached-behaviors-vs-attached.html"/>
	/// Des Weiteren gibt es auf dieser Seite zu Attached Properies einen kleinen Überblick: <seealso cref="https://www.wpftutorial.net/AttachedProperties.html"/>
	/// </summary>
	public class WeightedPanel : Panel
	{
		private double[] _normalWeights;

		public static readonly DependencyProperty WeightProperty = DependencyProperty.RegisterAttached("Weight", typeof(double), typeof(WeightedPanel), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));

		public static void SetWeight(DependencyObject obj, double weight)
		{
			obj.SetValue(WeightProperty, weight);
		}

		public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(WeightedPanel), new FrameworkPropertyMetadata(Orientation.Horizontal, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

		public Orientation Orientation
		{
			get { return (Orientation)GetValue(OrientationProperty); }
			set { SetValue(OrientationProperty, value); }
		}

		

		protected override Size MeasureOverride(Size constraint)
		{
			if (constraint.Width == double.PositiveInfinity || constraint.Height == double.PositiveInfinity)
				return Size.Empty;

			Rect[] rects = CalculateItemRects(constraint);
			for (int i = 0; i < InternalChildren.Count; i++)
			{
				InternalChildren[i].Measure(rects[i].Size);
			}

			return constraint;
		}

		protected override Size ArrangeOverride(Size finalSize)
		{
			Rect[] rects = CalculateItemRects(finalSize);
			for (int i = 0; i < InternalChildren.Count; i++)
			{
				InternalChildren[i].Arrange(rects[i]);
			}

			return finalSize;
		}

		private Rect[] CalculateItemRects(Size panelSize)
		{
			NormalizeWeights();

			Rect[] rects = new Rect[InternalChildren.Count];
			double offset = 0;
			for (int i = 0; i < InternalChildren.Count; i++)
			{
				if (Orientation == Orientation.Horizontal)
				{
					double width = panelSize.Width * _normalWeights[i];
					rects[i] = new Rect(offset, 0, width, panelSize.Height);
					offset += width;
				}
				else if (Orientation == Orientation.Vertical)
				{
					double height = panelSize.Height * _normalWeights[i];
					rects[i] = new Rect(0, offset, panelSize.Width, height);
					offset += height;
				}
			}

			return rects;
		}

		private void NormalizeWeights()
		{
			double weightSum = 0;
			foreach (UIElement child in InternalChildren)
			{
				weightSum += (double)child.GetValue(WeightProperty);
			}

			_normalWeights = new double[InternalChildren.Count];
			for (int i = 0; i < InternalChildren.Count; i++)
			{
				// Hier wird zu jedem Kind das Verhältnis für die Breite berechnet (über die Gewichtung)
				_normalWeights[i] = (double)InternalChildren[i].GetValue(WeightProperty) / weightSum;
			}
		}
	}
}