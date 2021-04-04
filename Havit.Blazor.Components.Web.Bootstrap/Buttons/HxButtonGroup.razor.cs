﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Havit.Blazor.Components.Web.Bootstrap
{
	/// <summary>
	/// Bootstrap Button group. https://getbootstrap.com/docs/5.0/components/button-group/
	/// </summary>
	public partial class HxButtonGroup
	{
		[Parameter] public RenderFragment ChildContent { get; set; }

		/// <summary>
		/// Size. Default is <see cref="ButtonGroupSize.Regular"/>.
		/// </summary>
		[Parameter] public ButtonGroupSize Size { get; set; } = ButtonGroupSize.Regular;

		/// <summary>
		/// Orientation. Default is <see cref="ButtonGroupOrientation.Horizontal"/>.
		/// </summary>
		[Parameter] public ButtonGroupOrientation Orientation { get; set; } = ButtonGroupOrientation.Horizontal;

		/// <summary>
		/// Groups should be given an explicit label, as most assistive technologies will otherwise not announce them, despite the presence of the correct role attribute. 
		/// </summary>
		[Parameter] public string AriaLabel { get; set; }

		/// <summary>
		/// Additional CSS class(es) to be added.
		/// </summary>
		[Parameter] public string CssClass { get; set; }

		protected string GetCoreCssClass()
		{
			if (Orientation == ButtonGroupOrientation.Vertical)
			{
				return "btn-group-vertical";
			}
			return "btn-group";
		}

		protected string GetSizeCssClass()
		{
			return Size switch
			{
				ButtonGroupSize.Regular => null,
				ButtonGroupSize.Small => "btn-group-sm",
				ButtonGroupSize.Large => "btn-group-lg",
				_ => throw new InvalidOperationException($"Unknown {nameof(ButtonGroupSize)}: {Size}.")
			};
		}
	}
}
