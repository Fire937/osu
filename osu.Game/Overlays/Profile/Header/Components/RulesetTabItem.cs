﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Graphics.UserInterface;
using osu.Game.Rulesets;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Overlays.Profile.Header.Components
{
    public class RulesetTabItem : TabItem<RulesetInfo>
    {
        private readonly OsuSpriteText text;
        private readonly SpriteIcon icon;

        private Color4 accentColour;

        public Color4 AccentColour
        {
            get => accentColour;
            set
            {
                if (accentColour == value)
                    return;

                accentColour = value;

                updateState();
            }
        }

        private bool isDefault;

        public bool IsDefault
        {
            get => isDefault;
            set
            {
                if (isDefault == value)
                    return;

                isDefault = value;

                icon.FadeTo(isDefault ? 1 : 0, 200, Easing.OutQuint);
            }
        }

        public RulesetTabItem(RulesetInfo value)
            : base(value)
        {
            AutoSizeAxes = Axes.Both;

            Children = new Drawable[]
            {
                new FillFlowContainer
                {
                    AutoSizeAxes = Axes.Both,
                    Origin = Anchor.BottomLeft,
                    Anchor = Anchor.BottomLeft,
                    Direction = FillDirection.Horizontal,
                    Spacing = new Vector2(3, 0),
                    Children = new Drawable[]
                    {
                        text = new OsuSpriteText
                        {
                            Origin = Anchor.Centre,
                            Anchor = Anchor.Centre,
                            Text = value.Name,
                            Font = OsuFont.GetFont()
                        },
                        icon = new SpriteIcon
                        {
                            Origin = Anchor.Centre,
                            Anchor = Anchor.Centre,
                            Alpha = 0,
                            AlwaysPresent = true,
                            Icon = FontAwesome.Solid.Star,
                            Size = new Vector2(12),
                        },
                    }
                },
                new HoverClickSounds()
            };
        }

        protected override bool OnHover(HoverEvent e)
        {
            base.OnHover(e);

            if (!Active.Value)
                hoverAction();

            return true;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            base.OnHoverLost(e);

            if (!Active.Value)
                unhoverAction();
        }

        protected override void OnActivated()
        {
            hoverAction();
            text.Font = text.Font.With(weight: FontWeight.Bold);
        }

        protected override void OnDeactivated()
        {
            unhoverAction();
            text.Font = text.Font.With(weight: FontWeight.Medium);
        }

        private void updateState()
        {
            if (Active.Value)
                OnActivated();
            else
                OnDeactivated();
        }

        private void hoverAction()
        {
            text.FadeColour(Color4.White, 120, Easing.InQuad);
            icon.FadeColour(Color4.White, 120, Easing.InQuad);
        }

        private void unhoverAction()
        {
            text.FadeColour(AccentColour, 120, Easing.InQuad);
            icon.FadeColour(AccentColour, 120, Easing.InQuad);
        }
    }
}
