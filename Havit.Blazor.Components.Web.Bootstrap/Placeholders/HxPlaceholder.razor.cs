﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web.Bootstrap.Layouts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace Havit.Blazor.Components.Web.Bootstrap
{
	/// <summary>
	/// <see href="https://getbootstrap.com/docs/5.1/components/placeholders/">Bootstrap 5 Placeholder</see> component, aka Skeleton.<br/>
	/// Use loading placeholders for your components or pages to indicate something may still be loading.
	/// </summary>
	public partial class HxPlaceholder : ILayoutColumnComponent
	{
		/// <inheritdoc cref="ILayoutColumnComponent.Columns"/>
		[Parameter] public string Columns { get; set; }

		/// <inheritdoc cref="ILayoutColumnComponent.ColumnsSmallUp"/>
		[Parameter] public string ColumnsSmallUp { get; set; }

		/// <inheritdoc cref="ILayoutColumnComponent.ColumnsMediumUp"/>
		[Parameter] public string ColumnsMediumUp { get; set; }

		/// <inheritdoc cref="ILayoutColumnComponent.ColumnsLargeUp"/>
		[Parameter] public string ColumnsLargeUp { get; set; }

		/// <inheritdoc cref="ILayoutColumnComponent.ColumnsExtraLargeUp"/>
		[Parameter] public string ColumnsExtraLargeUp { get; set; }

		/// <inheritdoc cref="ILayoutColumnComponent.ColumnsXxlUp"/>
		[Parameter] public string ColumnsXxlUp { get; set; }

		/// <summary>
		/// Optional content of the placeholder (usualy not used).
		/// </summary>
		[Parameter] public RenderFragment ChildContent { get; set; }

		/// <summary>
		/// Additional CSS class.
		/// </summary>
		[Parameter] public string CssClass { get; set; }

		/// <summary>
		/// Additional attributes to be splatted onto an underlying HTML element.
		/// </summary>
		[Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object> AdditionalAttributes { get; set; }

		protected virtual string GetCssClass()
		{
			return CssClassHelper.Combine(
				"placeholder",
				this.GetColumnsCssClasses(),
				this.CssClass);
		}
	}
}
