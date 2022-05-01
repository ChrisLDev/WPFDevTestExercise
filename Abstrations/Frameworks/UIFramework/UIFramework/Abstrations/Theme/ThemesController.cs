using ReactiveUI.Fody.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Baml2006;
using System.Windows.Markup;
using UIFramework.Abstrations.Theme;

namespace UIFramework
{
	public class  ThemesController : IThemesController
	{
		private ThemeResourceDictionary? _currentTheme;

        public ThemesController()
        {
            AvailableThemes = GetThemes();

            CurrentTheme = _currentTheme ?? AvailableThemes[0];
        }

        private ThemeResourceDictionary[] GetThemes()
		{
			var assembly = Assembly.GetExecutingAssembly();

			return assembly?.GetManifestResourceNames()
									   .SelectMany(resourceName =>
									   {
										   using var stream = assembly.GetManifestResourceStream(resourceName);
										   if (stream is null)
											   return Array.Empty<ThemeResourceDictionary>();

										   using var reader = new ResourceReader(stream);
										   return reader.OfType<DictionaryEntry>()
														.Where(e => e.Key is string s &&
																	s.StartsWith("Themes",
																				 StringComparison
																					.CurrentCultureIgnoreCase))
														.Select(e => e.Value)
														.OfType<Stream>()
														.Select(entryStream =>
														{
															var baml2006Reader = new Baml2006Reader(entryStream);
															return XamlReader.Load(baml2006Reader);
														})
														.OfType<ThemeResourceDictionary>()
														.ToArray();
									   })
									   .ToArray()
							  ?? Array.Empty<ThemeResourceDictionary>();
		}

		public Action? ThemeChanged;

		public IReadOnlyList<ThemeResourceDictionary> AvailableThemes { get; }

		public ThemeResourceDictionary? CurrentTheme
		{
			get => _currentTheme;
			set
			{
                if (value == null)
                {
					return;
                }

				Application.Current.Resources.MergedDictionaries.Remove(_currentTheme);

				_currentTheme = value;

                if (!Application.Current.Resources.MergedDictionaries.Contains(value))
                {
                    Application.Current.Resources.MergedDictionaries.Add(value);
                }
            }
		}
	}
}
